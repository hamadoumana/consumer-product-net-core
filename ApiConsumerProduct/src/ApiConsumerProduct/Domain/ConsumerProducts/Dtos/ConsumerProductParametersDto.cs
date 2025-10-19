namespace ApiConsumerProduct.Domain.ConsumerProducts.Dtos;

using ApiConsumerProduct.Resources;

public sealed class ConsumerProductParametersDto : BasePaginationParameters
{
    public string? Filters { get; set; }
    public string? SortOrder { get; set; }
}
