namespace ApiConsumerProduct.IntegrationTests.FeatureTests.ConsumerProducts;

using ApiConsumerProduct.SharedTestHelpers.Fakes.ConsumerProduct;
using ApiConsumerProduct.Domain.ConsumerProducts.Features;
using Microsoft.EntityFrameworkCore;
using Domain;
using System.Threading.Tasks;

public class DeleteConsumerProductCommandTests : TestBase
{
    [Fact]
    public async Task can_delete_consumerproduct_from_db()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var consumerProduct = new FakeConsumerProductBuilder().Build();
        await testingServiceScope.InsertAsync(consumerProduct);

        // Act
        var command = new DeleteConsumerProduct.Command(consumerProduct.Id);
        await testingServiceScope.SendAsync(command);
        var consumerProductResponse = await testingServiceScope
            .ExecuteDbContextAsync(db => db.ConsumerProducts
                .CountAsync(c => c.Id == consumerProduct.Id));

        // Assert
        consumerProductResponse.Should().Be(0);
    }

    [Fact]
    public async Task delete_consumerproduct_throws_notfoundexception_when_record_does_not_exist()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var badId = Guid.NewGuid();

        // Act
        var command = new DeleteConsumerProduct.Command(badId);
        Func<Task> act = () => testingServiceScope.SendAsync(command);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task can_softdelete_consumerproduct_from_db()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var consumerProduct = new FakeConsumerProductBuilder().Build();
        await testingServiceScope.InsertAsync(consumerProduct);

        // Act
        var command = new DeleteConsumerProduct.Command(consumerProduct.Id);
        await testingServiceScope.SendAsync(command);
        var deletedConsumerProduct = await testingServiceScope.ExecuteDbContextAsync(db => db.ConsumerProducts
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(x => x.Id == consumerProduct.Id));

        // Assert
        deletedConsumerProduct?.IsDeleted.Should().BeTrue();
    }
}