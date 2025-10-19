namespace ApiConsumerProduct.IntegrationTests.FeatureTests.ConsumerProducts;

using ApiConsumerProduct.SharedTestHelpers.Fakes.ConsumerProduct;
using Domain;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ApiConsumerProduct.Domain.ConsumerProducts.Features;

public class AddConsumerProductCommandTests : TestBase
{
    [Fact]
    public async Task can_add_new_consumerproduct_to_db()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var consumerProductOne = new FakeConsumerProductForCreationDto().Generate();

        // Act
        var command = new AddConsumerProduct.Command(consumerProductOne);
        var consumerProductReturned = await testingServiceScope.SendAsync(command);
        var consumerProductCreated = await testingServiceScope.ExecuteDbContextAsync(db => db.ConsumerProducts
            .FirstOrDefaultAsync(c => c.Id == consumerProductReturned.Id));

        // Assert
        consumerProductReturned.Name.Should().Be(consumerProductOne.Name);
        consumerProductReturned.Description.Should().Be(consumerProductOne.Description);

        consumerProductCreated.Name.Should().Be(consumerProductOne.Name);
        consumerProductCreated.Description.Should().Be(consumerProductOne.Description);
    }
}