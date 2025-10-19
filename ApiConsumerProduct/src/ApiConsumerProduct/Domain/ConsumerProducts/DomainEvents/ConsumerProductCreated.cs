namespace ApiConsumerProduct.Domain.ConsumerProducts.DomainEvents;

public sealed class ConsumerProductCreated : DomainEvent
{
    public ConsumerProduct ConsumerProduct { get; set; } 
}
            