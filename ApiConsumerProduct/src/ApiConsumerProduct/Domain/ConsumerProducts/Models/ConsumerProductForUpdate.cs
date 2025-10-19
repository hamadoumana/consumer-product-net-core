namespace ApiConsumerProduct.Domain.ConsumerProducts.Models;

using Destructurama.Attributed;

public sealed record ConsumerProductForUpdate
{
    public string Name { get; set; }
    public string Description { get; set; }
}
