namespace ApiConsumerProduct.Domain.ConsumerProducts.Features;

using ApiConsumerProduct.Databases;
using ApiConsumerProduct.Domain.ConsumerProducts;
using ApiConsumerProduct.Domain.ConsumerProducts.Dtos;
using ApiConsumerProduct.Domain.ConsumerProducts.Models;
using ApiConsumerProduct.Services;
using ApiConsumerProduct.Exceptions;
using Mappings;
using MediatR;

public static class AddConsumerProduct
{
    public sealed record Command(ConsumerProductForCreationDto ConsumerProductToAdd) : IRequest<ConsumerProductDto>;

    public sealed class Handler(ConsumerProductDbContext dbContext)
        : IRequestHandler<Command, ConsumerProductDto>
    {
        public async Task<ConsumerProductDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var consumerProductToAdd = request.ConsumerProductToAdd.ToConsumerProductForCreation();
            var consumerProduct = ConsumerProduct.Create(consumerProductToAdd);

            await dbContext.ConsumerProducts.AddAsync(consumerProduct, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            return consumerProduct.ToConsumerProductDto();
        }
    }
}