namespace ApiConsumerProduct.Domain.Users.Features;

using ApiConsumerProduct.Domain.Users.Dtos;
using ApiConsumerProduct.Databases;
using ApiConsumerProduct.Exceptions;
using ApiConsumerProduct.Domain;
using HeimGuard;
using Mappings;
using MediatR;
using Microsoft.EntityFrameworkCore;

public static class GetUser
{
    public sealed record Query(Guid UserId) : IRequest<UserDto>;

    public sealed class Handler(ConsumerProductDbContext dbContext, IHeimGuardClient heimGuard)
        : IRequestHandler<Query, UserDto>
    {
        public async Task<UserDto> Handle(Query request, CancellationToken cancellationToken)
        {
            await heimGuard.MustHavePermission<ForbiddenAccessException>(Permissions.CanGetUsers);

            var result = await dbContext.Users
                .AsNoTracking()
                .GetById(request.UserId, cancellationToken);
            return result.ToUserDto();
        }
    }
}