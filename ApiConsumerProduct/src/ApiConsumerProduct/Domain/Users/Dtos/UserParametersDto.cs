namespace ApiConsumerProduct.Domain.Users.Dtos;

using ApiConsumerProduct.Resources;

public sealed class UserParametersDto : BasePaginationParameters
{
    public string? Filters { get; set; }
    public string? SortOrder { get; set; }
}
