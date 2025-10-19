namespace ApiConsumerProduct.SharedTestHelpers.Fakes.ConsumerProduct;

using AutoBogus;
using ApiConsumerProduct.Domain.ConsumerProducts;
using ApiConsumerProduct.Domain.ConsumerProducts.Dtos;

public sealed class FakeConsumerProductForCreationDto : AutoFaker<ConsumerProductForCreationDto>
{
    public FakeConsumerProductForCreationDto()
    {
    }
}