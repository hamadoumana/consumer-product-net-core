namespace ApiConsumerProduct.Domain.ConsumerProducts.Dtos;

using Destructurama.Attributed;

public sealed record ConsumerProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}
