namespace ApiConsumerProduct.UnitTests.Domain.ConsumerProducts;

using ApiConsumerProduct.SharedTestHelpers.Fakes.ConsumerProduct;
using ApiConsumerProduct.Domain.ConsumerProducts;
using ApiConsumerProduct.Domain.ConsumerProducts.DomainEvents;
using Bogus;
using FluentAssertions.Extensions;
using ValidationException = ApiConsumerProduct.Exceptions.ValidationException;

public class CreateConsumerProductTests
{
    private readonly Faker _faker;

    public CreateConsumerProductTests()
    {
        _faker = new Faker();
    }
    
    [Fact]
    public void can_create_valid_consumerProduct()
    {
        // Arrange
        var consumerProductToCreate = new FakeConsumerProductForCreation().Generate();
        
        // Act
        var consumerProduct = ConsumerProduct.Create(consumerProductToCreate);

        // Assert
        consumerProduct.Name.Should().Be(consumerProductToCreate.Name);
        consumerProduct.Description.Should().Be(consumerProductToCreate.Description);
    }

    [Fact]
    public void queue_domain_event_on_create()
    {
        // Arrange
        var consumerProductToCreate = new FakeConsumerProductForCreation().Generate();
        
        // Act
        var consumerProduct = ConsumerProduct.Create(consumerProductToCreate);

        // Assert
        consumerProduct.DomainEvents.Count.Should().Be(1);
        consumerProduct.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(ConsumerProductCreated));
    }
}