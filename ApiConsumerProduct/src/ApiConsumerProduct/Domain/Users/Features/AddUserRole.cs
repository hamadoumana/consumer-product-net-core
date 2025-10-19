namespace ApiConsumerProduct.Domain.Users.Features;

using ApiConsumerProduct.Databases;
using ApiConsumerProduct.Domain.Users;
using ApiConsumerProduct.Domain.Users.Dtos;
using ApiConsumerProduct.Services;
using ApiConsumerProduct.Exceptions;
using HeimGuard;
using Mappings;
using MediatR;
using Roles;

public static class AddUserRole
{
    public sealed record Command(Guid UserId, string Role, bool SkipPermissions = false) : IRequest;

    public sealed class Handler(ConsumerProductDbContext dbContext, IHeimGuardClient heimGuard) : IRequestHandler<Command>
    {
        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            if(!request.SkipPermissions)
                await heimGuard.MustHavePermission<ForbiddenAccessException>(Permissions.CanAddUserRoles);
            
            var user = await dbContext.GetUserAggregate().GetById(request.UserId, cancellationToken);

            var roleToAdd = user.AddRole(new Role(request.Role));
            await dbContext.UserRoles.AddAsync(roleToAdd, cancellationToken);

            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}