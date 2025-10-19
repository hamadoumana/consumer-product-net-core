namespace ApiConsumerProduct.Domain.RolePermissions.Features;

using ApiConsumerProduct.Databases;
using ApiConsumerProduct.Domain.RolePermissions;
using ApiConsumerProduct.Domain.RolePermissions.Dtos;
using ApiConsumerProduct.Domain.RolePermissions.Models;
using ApiConsumerProduct.Services;
using ApiConsumerProduct.Exceptions;
using ApiConsumerProduct.Domain;
using HeimGuard;
using Mappings;
using MediatR;

public static class AddRolePermission
{
    public sealed record Command(RolePermissionForCreationDto RolePermissionToAdd) : IRequest<RolePermissionDto>;

    public sealed class Handler(ConsumerProductDbContext dbContext, IHeimGuardClient heimGuard)
        : IRequestHandler<Command, RolePermissionDto>
    {
        public async Task<RolePermissionDto> Handle(Command request, CancellationToken cancellationToken)
        {
            await heimGuard.MustHavePermission<ForbiddenAccessException>(Permissions.CanAddPermissions);

            var rolePermissionToAdd = request.RolePermissionToAdd.ToRolePermissionForCreation();
            var rolePermission = RolePermission.Create(rolePermissionToAdd);

            await dbContext.RolePermissions.AddAsync(rolePermission, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            return rolePermission.ToRolePermissionDto();
        }
    }
}