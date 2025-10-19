namespace ApiConsumerProduct.Domain.ConsumerProducts.Features;

using ApiConsumerProduct.Domain.ConsumerProducts;
using ApiConsumerProduct.Domain.ConsumerProducts.Dtos;
using ApiConsumerProduct.Databases;
using ApiConsumerProduct.Services;
using ApiConsumerProduct.Domain.ConsumerProducts.Models;
using ApiConsumerProduct.Exceptions;
using Mappings;
using MediatR;

public static class UpdateConsumerProduct
{
    public sealed record Command(Guid ConsumerProductId, ConsumerProductForUpdateDto UpdatedConsumerProductData) : IRequest;

    public sealed class Handler(ConsumerProductDbContext dbContext)
        : IRequestHandler<Command>
    {
        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            var consumerProductToUpdate = await dbContext.ConsumerProducts.GetById(request.ConsumerProductId, cancellationToken: cancellationToken);
            var consumerProductToAdd = request.UpdatedConsumerProductData.ToConsumerProductForUpdate();
            consumerProductToUpdate.Update(consumerProductToAdd);

            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}