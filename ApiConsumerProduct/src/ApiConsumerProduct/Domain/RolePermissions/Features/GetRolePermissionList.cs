namespace ApiConsumerProduct.Domain.RolePermissions.Features;

using ApiConsumerProduct.Domain.RolePermissions.Dtos;
using ApiConsumerProduct.Databases;
using ApiConsumerProduct.Exceptions;
using ApiConsumerProduct.Resources;
using ApiConsumerProduct.Domain;
using HeimGuard;
using Mappings;
using Microsoft.EntityFrameworkCore;
using MediatR;
using QueryKit;
using QueryKit.Configuration;

public static class GetRolePermissionList
{
    public sealed record Query(RolePermissionParametersDto QueryParameters) : IRequest<PagedList<RolePermissionDto>>;

    public sealed class Handler(ConsumerProductDbContext dbContext, IHeimGuardClient heimGuard)
        : IRequestHandler<Query, PagedList<RolePermissionDto>>
    {
        public async Task<PagedList<RolePermissionDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            await heimGuard.MustHavePermission<ForbiddenAccessException>(Permissions.CanReadRolePermissions);

            var collection = dbContext.RolePermissions.AsNoTracking();

            var queryKitConfig = new CustomQueryKitConfiguration();
            var queryKitData = new QueryKitData()
            {
                Filters = request.QueryParameters.Filters,
                SortOrder = request.QueryParameters.SortOrder,
                Configuration = queryKitConfig
            };
            var appliedCollection = collection.ApplyQueryKit(queryKitData);
            var dtoCollection = appliedCollection.ToRolePermissionDtoQueryable();

            return await PagedList<RolePermissionDto>.CreateAsync(dtoCollection,
                request.QueryParameters.PageNumber,
                request.QueryParameters.PageSize,
                cancellationToken);
        }
    }
}