namespace ApiConsumerProduct.SharedTestHelpers.Fakes.ConsumerProduct;

using AutoBogus;
using ApiConsumerProduct.Domain.ConsumerProducts;
using ApiConsumerProduct.Domain.ConsumerProducts.Dtos;

public sealed class FakeConsumerProductForUpdateDto : AutoFaker<ConsumerProductForUpdateDto>
{
    public FakeConsumerProductForUpdateDto()
    {
    }
}