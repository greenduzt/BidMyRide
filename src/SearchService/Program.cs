using System.Net;
using MassTransit;
using Polly;
using Polly.Extensions.Http;
using SearchService.Consumers;
using SearchService.Data;
using SearchService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddHttpClient<AuctionSvcHttpClient>().AddPolicyHandler(GetPolicy());
builder.Services.AddMassTransit(x => {

    x.AddConsumersFromNamespaceContaining<AuctionCreatedConsumer>();
   
    x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("search", false));

    x.UsingRabbitMq((context, cfg) => {

        cfg.ReceiveEndpoint("search-auction-created", e =>
        {
            e.UseMessageRetry(r => r.Interval(5,5));


            e.ConfigureConsumer<AuctionCreatedConsumer>(context);
        });

        cfg.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Lifetime.ApplicationStarted.Register(async () =>{
    try
    {
        await DbInitializer.InitDb(app);
    }
    catch (Exception e)
    {
        
        System.Console.WriteLine(e);;
    }
});

app.Run();
 
// If the AuctionService is down, we are going to handle the exception
// and keep on trying evry 3 seconds until the AuctionService is up and running.
// This ensures the policy will retry only on temporary failures and not on every HTTP error.
// This ensures that if the HTTP response status is 404 Not Found, Polly will still retry.

static IAsyncPolicy<HttpResponseMessage> GetPolicy()
    => HttpPolicyExtensions
        .HandleTransientHttpError()// Handles transient HTTP errors such as 5xxx (Server errors), 408 (Request Timeout), Any network failures such as timeouts, connection failures or DNS issues.
        .OrResult(msg => msg.StatusCode == HttpStatusCode.NotFound)// This method extends the previous one by also handling 404 Not Found responses. .OrResult(...) function specify a custom condition where Polly should also retry
        .WaitAndRetryForeverAsync(_ => TimeSpan.FromSeconds(3));