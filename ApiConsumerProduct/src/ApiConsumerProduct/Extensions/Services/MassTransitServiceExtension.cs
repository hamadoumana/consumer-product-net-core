namespace ApiConsumerProduct.Extensions.Services;

using ApiConsumerProduct.Resources;
using ApiConsumerProduct.Services;
using SharedKernel.Messages;
using Resources;
using MassTransit;
using ApiConsumerProduct.Extensions.Services.ConsumerRegistrations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

public static class MassTransitServiceExtension
{
    public static void AddMassTransitServices(this IServiceCollection services, IWebHostEnvironment env, IConfiguration configuration)
    {
        var rmqOptions = configuration.GetRabbitMqOptions();

        if (!env.IsEnvironment(Consts.Testing.IntegrationTestingEnvName) 
            && !env.IsEnvironment(Consts.Testing.FunctionalTestingEnvName))
        {
            services.AddMassTransit(mt =>
            {
                mt.AddConsumers(Assembly.GetExecutingAssembly());
                mt.UsingRabbitMq((context, cfg) =>
                {
					cfg.Host(Environment.GetEnvironmentVariable("RMQ_HOST"),
					   ushort.Parse(Environment.GetEnvironmentVariable("RMQ_PORT")),
					   Environment.GetEnvironmentVariable("RMQ_VIRTUAL_HOST"),
					   h =>
					   {
						   h.Username(Environment.GetEnvironmentVariable("RMQ_USERNAME"));
						   h.Password(Environment.GetEnvironmentVariable("RMQ_PASSWORD"));
					   });

					// Producers -- Do Not Delete This Comment

					// Consumers -- Do Not Delete This Comment
					cfg.CreatedProductCusumerEndpointRegistration(context);
                });
            });
            services.AddOptions<MassTransitHostOptions>();
        }
    }
}
