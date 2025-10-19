namespace ApiConsumerProduct.IntegrationTests.FeatureTests.ConsumerProducts;

using ApiConsumerProduct.SharedTestHelpers.Fakes.ConsumerProduct;
using ApiConsumerProduct.Domain.ConsumerProducts.Features;
using Domain;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

public class ConsumerProductQueryTests : TestBase
{
    [Fact]
    public async Task can_get_existing_consumerproduct_with_accurate_props()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var consumerProductOne = new FakeConsumerProductBuilder().Build();
        await testingServiceScope.InsertAsync(consumerProductOne);

        // Act
        var query = new GetConsumerProduct.Query(consumerProductOne.Id);
        var consumerProduct = await testingServiceScope.SendAsync(query);

        // Assert
        consumerProduct.Name.Should().Be(consumerProductOne.Name);
        consumerProduct.Description.Should().Be(consumerProductOne.Description);
    }

    [Fact]
    public async Task get_consumerproduct_throws_notfound_exception_when_record_does_not_exist()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var badId = Guid.NewGuid();

        // Act
        var query = new GetConsumerProduct.Query(badId);
        Func<Task> act = () => testingServiceScope.SendAsync(query);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}