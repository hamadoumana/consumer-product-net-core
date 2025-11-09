namespace ApiConsumerProduct.Extensions.Services.ConsumerRegistrations;

using MassTransit;
using MassTransit.RabbitMqTransport;
using RabbitMQ.Client;
using ApiConsumerProduct.Domain.DomainDirectory.Features;

public static class CreatedProductCusumerEndpointRegistrationRegistration
{
    public static void CreatedProductCusumerEndpointRegistration(this IRabbitMqBusFactoryConfigurator cfg, IBusRegistrationContext context)
    {
        cfg.ReceiveEndpoint(Environment.GetEnvironmentVariable("RMQ_QUEUE_NAME"), re =>
        {
            // turns off default fanout settings
            re.ConfigureConsumeTopology = false;

            // a replicated queue to provide high availability and data safety. available in RMQ 3.8+
          //  re.SetQuorumQueue();

            // enables a lazy queue for more stable cluster with better predictive performance.
            // Please note that you should disable lazy queues if you require really high performance, if the queues are always short, or if you have set a max-length policy.
            re.SetQueueArgument("declare", "lazy");

            // the consumers that are subscribed to the endpoint
            re.ConfigureConsumer<CreatedProductConsumer>(context);

            // the binding of the intermediary exchange and the primary exchange
            re.Bind("created-generation-product-exchange", e =>
            {
                e.ExchangeType = ExchangeType.Fanout;
            });
        });
    }
}