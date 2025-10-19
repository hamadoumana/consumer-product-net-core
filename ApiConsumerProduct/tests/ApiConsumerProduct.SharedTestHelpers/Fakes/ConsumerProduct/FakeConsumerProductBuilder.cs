namespace ApiConsumerProduct.SharedTestHelpers.Fakes.ConsumerProduct;

using ApiConsumerProduct.Domain.ConsumerProducts;
using ApiConsumerProduct.Domain.ConsumerProducts.Models;

public class FakeConsumerProductBuilder
{
    private ConsumerProductForCreation _creationData = new FakeConsumerProductForCreation().Generate();

    public FakeConsumerProductBuilder WithModel(ConsumerProductForCreation model)
    {
        _creationData = model;
        return this;
    }
    
    public FakeConsumerProductBuilder WithName(string name)
    {
        _creationData.Name = name;
        return this;
    }
    
    public FakeConsumerProductBuilder WithDescription(string description)
    {
        _creationData.Description = description;
        return this;
    }
    
    public ConsumerProduct Build()
    {
        var result = ConsumerProduct.Create(_creationData);
        return result;
    }
}