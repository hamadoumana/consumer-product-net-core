namespace ApiConsumerProduct.Domain.RolePermissions.Features;

using ApiConsumerProduct.Domain.RolePermissions;
using ApiConsumerProduct.Domain.RolePermissions.Dtos;
using ApiConsumerProduct.Databases;
using ApiConsumerProduct.Services;
using ApiConsumerProduct.Domain.RolePermissions.Models;
using ApiConsumerProduct.Exceptions;
using ApiConsumerProduct.Domain;
using HeimGuard;
using Mappings;
using MediatR;

public static class UpdateRolePermission
{
    public sealed record Command(Guid RolePermissionId, RolePermissionForUpdateDto UpdatedRolePermissionData) : IRequest;

    public sealed class Handler(ConsumerProductDbContext dbContext, IHeimGuardClient heimGuard)
        : IRequestHandler<Command>
    {
        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            await heimGuard.MustHavePermission<ForbiddenAccessException>(Permissions.CanUpdatePermissions);

            var rolePermissionToUpdate = await dbContext.RolePermissions.GetById(request.RolePermissionId, cancellationToken: cancellationToken);
            var rolePermissionToAdd = request.UpdatedRolePermissionData.ToRolePermissionForUpdate();
            rolePermissionToUpdate.Update(rolePermissionToAdd);

            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}