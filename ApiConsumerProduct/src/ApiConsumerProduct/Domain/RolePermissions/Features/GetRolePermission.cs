namespace ApiConsumerProduct.Domain.RolePermissions.Features;

using ApiConsumerProduct.Domain.RolePermissions.Dtos;
using ApiConsumerProduct.Databases;
using ApiConsumerProduct.Exceptions;
using ApiConsumerProduct.Domain;
using HeimGuard;
using Mappings;
using MediatR;
using Microsoft.EntityFrameworkCore;

public static class GetRolePermission
{
    public sealed record Query(Guid RolePermissionId) : IRequest<RolePermissionDto>;

    public sealed class Handler(ConsumerProductDbContext dbContext, IHeimGuardClient heimGuard)
        : IRequestHandler<Query, RolePermissionDto>
    {
        public async Task<RolePermissionDto> Handle(Query request, CancellationToken cancellationToken)
        {
            await heimGuard.MustHavePermission<ForbiddenAccessException>(Permissions.CanReadRolePermissions);

            var result = await dbContext.RolePermissions
                .AsNoTracking()
                .GetById(request.RolePermissionId, cancellationToken);
            return result.ToRolePermissionDto();
        }
    }
}