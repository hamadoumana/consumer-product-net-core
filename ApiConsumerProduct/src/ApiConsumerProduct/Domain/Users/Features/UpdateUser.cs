namespace ApiConsumerProduct.Domain.Users.Features;

using ApiConsumerProduct.Domain.Users;
using ApiConsumerProduct.Domain.Users.Dtos;
using ApiConsumerProduct.Databases;
using ApiConsumerProduct.Services;
using ApiConsumerProduct.Domain.Users.Models;
using ApiConsumerProduct.Exceptions;
using ApiConsumerProduct.Domain;
using HeimGuard;
using Mappings;
using MediatR;

public static class UpdateUser
{
    public sealed record Command(Guid UserId, UserForUpdateDto UpdatedUserData) : IRequest;

    public sealed class Handler(ConsumerProductDbContext dbContext, IHeimGuardClient heimGuard)
        : IRequestHandler<Command>
    {
        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            await heimGuard.MustHavePermission<ForbiddenAccessException>(Permissions.CanUpdateUsers);

            var userToUpdate = await dbContext.Users.GetById(request.UserId, cancellationToken: cancellationToken);
            var userToAdd = request.UpdatedUserData.ToUserForUpdate();
            userToUpdate.Update(userToAdd);

            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}