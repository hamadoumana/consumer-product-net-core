namespace ApiConsumerProduct.Domain.ConsumerProducts.Features;

using ApiConsumerProduct.Domain.ConsumerProducts.Dtos;
using ApiConsumerProduct.Databases;
using ApiConsumerProduct.Exceptions;
using ApiConsumerProduct.Resources;
using Mappings;
using Microsoft.EntityFrameworkCore;
using MediatR;
using QueryKit;
using QueryKit.Configuration;

public static class GetConsumerProductList
{
    public sealed record Query(ConsumerProductParametersDto QueryParameters) : IRequest<PagedList<ConsumerProductDto>>;

    public sealed class Handler(ConsumerProductDbContext dbContext)
        : IRequestHandler<Query, PagedList<ConsumerProductDto>>
    {
        public async Task<PagedList<ConsumerProductDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var collection = dbContext.ConsumerProducts.AsNoTracking();

            var queryKitConfig = new CustomQueryKitConfiguration();
            var queryKitData = new QueryKitData()
            {
                Filters = request.QueryParameters.Filters,
                SortOrder = request.QueryParameters.SortOrder,
                Configuration = queryKitConfig
            };
            var appliedCollection = collection.ApplyQueryKit(queryKitData);
            var dtoCollection = appliedCollection.ToConsumerProductDtoQueryable();

            return await PagedList<ConsumerProductDto>.CreateAsync(dtoCollection,
                request.QueryParameters.PageNumber,
                request.QueryParameters.PageSize,
                cancellationToken);
        }
    }
}