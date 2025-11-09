namespace ApiConsumerProduct.Domain.DomainDirectory.Features;

using MassTransit;
using SharedKernel.Messages;
using System.Threading.Tasks;

public sealed class  CreatedProductConsumer() : IConsumer<ICreatedProductMessage>
{
    public Task Consume(ConsumeContext<ICreatedProductMessage> context)
    {
		// do work here
		var product = context.Message;
		
		Console.WriteLine($"Received CreatedProductMessage: ProductId={product.ProductId}, Price={product.Price}, Description={product.Description}, Quantity={product.Quantity}");

		return Task.CompletedTask;
    }
}