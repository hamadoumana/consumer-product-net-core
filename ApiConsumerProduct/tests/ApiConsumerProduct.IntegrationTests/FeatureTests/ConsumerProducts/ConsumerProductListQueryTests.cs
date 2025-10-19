namespace ApiConsumerProduct.IntegrationTests.FeatureTests.ConsumerProducts;

using ApiConsumerProduct.Domain.ConsumerProducts.Dtos;
using ApiConsumerProduct.SharedTestHelpers.Fakes.ConsumerProduct;
using ApiConsumerProduct.Domain.ConsumerProducts.Features;
using Domain;
using System.Threading.Tasks;

public class ConsumerProductListQueryTests : TestBase
{
    
    [Fact]
    public async Task can_get_consumerproduct_list()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var consumerProductOne = new FakeConsumerProductBuilder().Build();
        var consumerProductTwo = new FakeConsumerProductBuilder().Build();
        var queryParameters = new ConsumerProductParametersDto();

        await testingServiceScope.InsertAsync(consumerProductOne, consumerProductTwo);

        // Act
        var query = new GetConsumerProductList.Query(queryParameters);
        var consumerProducts = await testingServiceScope.SendAsync(query);

        // Assert
        consumerProducts.Count.Should().BeGreaterThanOrEqualTo(2);
    }
}