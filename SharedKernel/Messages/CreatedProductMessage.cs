namespace SharedKernel.Messages
{
    using System;
    using System.Text;

    public interface ICreatedProductMessage
    {
        public Guid ProductId { get; set; }

public double Price { get; set; }

public string Description { get; set; }

public int Quantity { get; set; }
    }

    public class CreatedProductMessage : ICreatedProductMessage
    {
        public Guid ProductId { get; set; }

public double Price { get; set; }

public string Description { get; set; }

public int Quantity { get; set; }
    }
}