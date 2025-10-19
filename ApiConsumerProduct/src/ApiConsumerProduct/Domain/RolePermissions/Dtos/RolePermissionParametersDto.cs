namespace ApiConsumerProduct.Domain.RolePermissions.Dtos;

using ApiConsumerProduct.Resources;

public sealed class RolePermissionParametersDto : BasePaginationParameters
{
    public string? Filters { get; set; }
    public string? SortOrder { get; set; }
}
