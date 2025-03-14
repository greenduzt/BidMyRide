using AutoMapper;
using Contracts;
using MassTransit;
using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Consumers;

public class AuctionCreatedConsumer(IMapper mapper) : IConsumer<AuctionCreated>
{
    public  async Task Consume(ConsumeContext<AuctionCreated> context)
    {

        System.Console.WriteLine("--> Consuming auction created: " + context.Message.Id);

        // Map data from context.Message to an instance of Item
        // context.Message is an incoming message from a message queue (RabbitMQ via MassTransit).
        var item = mapper.Map<Item>(context.Message);

        await item.SaveAsync();
    }
}
