namespace ApiConsumerProduct.Domain.ConsumerProducts.Dtos;

using Destructurama.Attributed;

public sealed record ConsumerProductForCreationDto
{
    public string Name { get; set; }
    public string Description { get; set; }
}
