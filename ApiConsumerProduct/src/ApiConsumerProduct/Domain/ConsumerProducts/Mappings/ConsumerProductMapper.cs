namespace ApiConsumerProduct.Domain.ConsumerProducts.Mappings;

using ApiConsumerProduct.Domain.ConsumerProducts.Dtos;
using ApiConsumerProduct.Domain.ConsumerProducts.Models;
using Riok.Mapperly.Abstractions;

[Mapper]
public static partial class ConsumerProductMapper
{
    public static partial ConsumerProductForCreation ToConsumerProductForCreation(this ConsumerProductForCreationDto consumerProductForCreationDto);
    public static partial ConsumerProductForUpdate ToConsumerProductForUpdate(this ConsumerProductForUpdateDto consumerProductForUpdateDto);
    public static partial ConsumerProductDto ToConsumerProductDto(this ConsumerProduct consumerProduct);
    public static partial IQueryable<ConsumerProductDto> ToConsumerProductDtoQueryable(this IQueryable<ConsumerProduct> consumerProduct);
}