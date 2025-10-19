namespace ApiConsumerProduct.Domain.Users.Features;

using ApiConsumerProduct.Databases;
using ApiConsumerProduct.Domain.Users;
using ApiConsumerProduct.Domain.Users.Dtos;
using ApiConsumerProduct.Domain.Users.Models;
using ApiConsumerProduct.Services;
using ApiConsumerProduct.Exceptions;
using ApiConsumerProduct.Domain;
using HeimGuard;
using Mappings;
using MediatR;

public static class AddUser
{
    public sealed record Command(UserForCreationDto UserToAdd, bool SkipPermissions = false) : IRequest<UserDto>;

    public sealed class Handler(ConsumerProductDbContext dbContext, IHeimGuardClient heimGuard)
        : IRequestHandler<Command, UserDto>
    {
        public async Task<UserDto> Handle(Command request, CancellationToken cancellationToken)
        {
            if(!request.SkipPermissions)
                await heimGuard.MustHavePermission<ForbiddenAccessException>(Permissions.CanAddUsers);

            var userToAdd = request.UserToAdd.ToUserForCreation();
            var user = User.Create(userToAdd);
            await dbContext.Users.AddAsync(user, cancellationToken);

            await dbContext.SaveChangesAsync(cancellationToken);

            var userAdded = await dbContext.Users.GetById(user.Id, cancellationToken);
            return userAdded.ToUserDto();
        }
    }
}
