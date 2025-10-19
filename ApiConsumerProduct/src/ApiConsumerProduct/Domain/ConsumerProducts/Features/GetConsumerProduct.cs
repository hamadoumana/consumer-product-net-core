namespace ApiConsumerProduct.Domain.ConsumerProducts.Features;

using ApiConsumerProduct.Domain.ConsumerProducts.Dtos;
using ApiConsumerProduct.Databases;
using ApiConsumerProduct.Exceptions;
using Mappings;
using MediatR;
using Microsoft.EntityFrameworkCore;

public static class GetConsumerProduct
{
    public sealed record Query(Guid ConsumerProductId) : IRequest<ConsumerProductDto>;

    public sealed class Handler(ConsumerProductDbContext dbContext)
        : IRequestHandler<Query, ConsumerProductDto>
    {
        public async Task<ConsumerProductDto> Handle(Query request, CancellationToken cancellationToken)
        {
            var result = await dbContext.ConsumerProducts
                .AsNoTracking()
                .GetById(request.ConsumerProductId, cancellationToken);
            return result.ToConsumerProductDto();
        }
    }
}