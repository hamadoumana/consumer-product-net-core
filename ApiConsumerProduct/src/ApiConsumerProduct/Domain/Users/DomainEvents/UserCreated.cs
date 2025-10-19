namespace ApiConsumerProduct.Domain.Users.DomainEvents;

public sealed class UserCreated : DomainEvent
{
    public User User { get; set; } 
}
            