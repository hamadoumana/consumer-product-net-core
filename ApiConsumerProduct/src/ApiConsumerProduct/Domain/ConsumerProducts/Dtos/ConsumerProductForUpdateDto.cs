namespace ApiConsumerProduct.Domain.ConsumerProducts.Dtos;

using Destructurama.Attributed;

public sealed record ConsumerProductForUpdateDto
{
    public string Name { get; set; }
    public string Description { get; set; }
}
