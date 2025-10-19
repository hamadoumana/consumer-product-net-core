namespace ApiConsumerProduct.Domain.ConsumerProducts;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Destructurama.Attributed;
using ApiConsumerProduct.Exceptions;
using ApiConsumerProduct.Domain.ConsumerProducts.Models;
using ApiConsumerProduct.Domain.ConsumerProducts.DomainEvents;


public class ConsumerProduct : BaseEntity
{
    [Column("Name")]
    public string Name { get; private set; }

    public string Description { get; private set; }

    // Add Props Marker -- Deleting this comment will cause the add props utility to be incomplete


    public static ConsumerProduct Create(ConsumerProductForCreation consumerProductForCreation)
    {
        var newConsumerProduct = new ConsumerProduct();

        newConsumerProduct.Name = consumerProductForCreation.Name;
        newConsumerProduct.Description = consumerProductForCreation.Description;

        newConsumerProduct.QueueDomainEvent(new ConsumerProductCreated(){ ConsumerProduct = newConsumerProduct });
        
        return newConsumerProduct;
    }

    public ConsumerProduct Update(ConsumerProductForUpdate consumerProductForUpdate)
    {
        Name = consumerProductForUpdate.Name;
        Description = consumerProductForUpdate.Description;

        QueueDomainEvent(new ConsumerProductUpdated(){ Id = Id });
        return this;
    }

    // Add Prop Methods Marker -- Deleting this comment will cause the add props utility to be incomplete
    
    protected ConsumerProduct() { } // For EF + Mocking
}