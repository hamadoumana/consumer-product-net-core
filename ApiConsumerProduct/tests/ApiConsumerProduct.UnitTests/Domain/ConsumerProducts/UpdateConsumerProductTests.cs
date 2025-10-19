namespace ApiConsumerProduct.UnitTests.Domain.ConsumerProducts;

using ApiConsumerProduct.SharedTestHelpers.Fakes.ConsumerProduct;
using ApiConsumerProduct.Domain.ConsumerProducts;
using ApiConsumerProduct.Domain.ConsumerProducts.DomainEvents;
using Bogus;
using FluentAssertions.Extensions;
using ValidationException = ApiConsumerProduct.Exceptions.ValidationException;

public class UpdateConsumerProductTests
{
    private readonly Faker _faker;

    public UpdateConsumerProductTests()
    {
        _faker = new Faker();
    }
    
    [Fact]
    public void can_update_consumerProduct()
    {
        // Arrange
        var consumerProduct = new FakeConsumerProductBuilder().Build();
        var updatedConsumerProduct = new FakeConsumerProductForUpdate().Generate();
        
        // Act
        consumerProduct.Update(updatedConsumerProduct);

        // Assert
        consumerProduct.Name.Should().Be(updatedConsumerProduct.Name);
        consumerProduct.Description.Should().Be(updatedConsumerProduct.Description);
    }
    
    [Fact]
    public void queue_domain_event_on_update()
    {
        // Arrange
        var consumerProduct = new FakeConsumerProductBuilder().Build();
        var updatedConsumerProduct = new FakeConsumerProductForUpdate().Generate();
        consumerProduct.DomainEvents.Clear();
        
        // Act
        consumerProduct.Update(updatedConsumerProduct);

        // Assert
        consumerProduct.DomainEvents.Count.Should().Be(1);
        consumerProduct.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(ConsumerProductUpdated));
    }
}