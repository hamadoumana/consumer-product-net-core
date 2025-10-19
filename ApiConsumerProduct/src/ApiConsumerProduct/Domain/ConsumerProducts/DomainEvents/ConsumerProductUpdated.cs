namespace ApiConsumerProduct.Domain.ConsumerProducts.DomainEvents;

public sealed class ConsumerProductUpdated : DomainEvent
{
    public Guid Id { get; set; } 
}
            