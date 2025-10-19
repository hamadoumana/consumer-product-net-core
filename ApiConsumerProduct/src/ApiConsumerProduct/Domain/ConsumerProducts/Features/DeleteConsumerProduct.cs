namespace ApiConsumerProduct.Domain.ConsumerProducts.Features;

using ApiConsumerProduct.Databases;
using ApiConsumerProduct.Services;
using ApiConsumerProduct.Exceptions;
using MediatR;

public static class DeleteConsumerProduct
{
    public sealed record Command(Guid ConsumerProductId) : IRequest;

    public sealed class Handler(ConsumerProductDbContext dbContext)
        : IRequestHandler<Command>
    {
        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            var recordToDelete = await dbContext.ConsumerProducts
                .GetById(request.ConsumerProductId, cancellationToken: cancellationToken);
            dbContext.Remove(recordToDelete);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}