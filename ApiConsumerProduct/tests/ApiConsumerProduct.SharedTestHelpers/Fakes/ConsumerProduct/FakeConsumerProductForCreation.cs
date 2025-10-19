namespace ApiConsumerProduct.SharedTestHelpers.Fakes.ConsumerProduct;

using AutoBogus;
using ApiConsumerProduct.Domain.ConsumerProducts;
using ApiConsumerProduct.Domain.ConsumerProducts.Models;

public sealed class FakeConsumerProductForCreation : AutoFaker<ConsumerProductForCreation>
{
    public FakeConsumerProductForCreation()
    {
    }
}