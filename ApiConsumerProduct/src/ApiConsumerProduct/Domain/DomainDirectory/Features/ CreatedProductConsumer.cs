namespace ApiConsumerProduct.Domain.DomainDirectory.Features;

using MassTransit;
using SharedKernel.Messages;
using System.Threading.Tasks;

public sealed class  CreatedProductConsumer() : IConsumer<ICreatedProductMessage>
{
    public Task Consume(ConsumeContext<ICreatedProductMessage> context)
    {
        // do work here

        return Task.CompletedTask;
    }
}