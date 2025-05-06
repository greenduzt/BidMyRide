using Contracts;
using MassTransit;

namespace AuctionService.Consumers;
public class AuctionCreatedFaultConsumer : IConsumer<Fault<AuctionCreated>>
{
    public async Task Consume(ConsumeContext<Fault<AuctionCreated>> context)
    {
        // Handle the fault message here
        var fault = context.Message;
        // Log the fault or take appropriate action
        Console.WriteLine($"Fault occurred: {fault.Message}");
    }
}
{
    
}