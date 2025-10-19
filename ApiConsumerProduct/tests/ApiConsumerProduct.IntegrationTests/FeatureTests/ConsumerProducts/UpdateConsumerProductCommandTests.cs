namespace ApiConsumerProduct.IntegrationTests.FeatureTests.ConsumerProducts;

using ApiConsumerProduct.SharedTestHelpers.Fakes.ConsumerProduct;
using ApiConsumerProduct.Domain.ConsumerProducts.Dtos;
using ApiConsumerProduct.Domain.ConsumerProducts.Features;
using Domain;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

public class UpdateConsumerProductCommandTests : TestBase
{
    [Fact]
    public async Task can_update_existing_consumerproduct_in_db()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var consumerProduct = new FakeConsumerProductBuilder().Build();
        await testingServiceScope.InsertAsync(consumerProduct);
        var updatedConsumerProductDto = new FakeConsumerProductForUpdateDto().Generate();

        // Act
        var command = new UpdateConsumerProduct.Command(consumerProduct.Id, updatedConsumerProductDto);
        await testingServiceScope.SendAsync(command);
        var updatedConsumerProduct = await testingServiceScope
            .ExecuteDbContextAsync(db => db.ConsumerProducts
                .FirstOrDefaultAsync(c => c.Id == consumerProduct.Id));

        // Assert
        updatedConsumerProduct.Name.Should().Be(updatedConsumerProductDto.Name);
        updatedConsumerProduct.Description.Should().Be(updatedConsumerProductDto.Description);
    }
}