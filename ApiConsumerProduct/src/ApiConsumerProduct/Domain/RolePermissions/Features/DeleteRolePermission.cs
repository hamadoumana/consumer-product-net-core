namespace ApiConsumerProduct.Domain.RolePermissions.Features;

using ApiConsumerProduct.Databases;
using ApiConsumerProduct.Services;
using ApiConsumerProduct.Exceptions;
using ApiConsumerProduct.Domain;
using HeimGuard;
using MediatR;

public static class DeleteRolePermission
{
    public sealed record Command(Guid RolePermissionId) : IRequest;

    public sealed class Handler(ConsumerProductDbContext dbContext, IHeimGuardClient heimGuard)
        : IRequestHandler<Command>
    {
        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            await heimGuard.MustHavePermission<ForbiddenAccessException>(Permissions.CanDeletePermissions);

            var recordToDelete = await dbContext.RolePermissions
                .GetById(request.RolePermissionId, cancellationToken: cancellationToken);
            dbContext.Remove(recordToDelete);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}