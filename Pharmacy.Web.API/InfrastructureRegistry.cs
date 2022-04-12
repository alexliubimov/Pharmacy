using Pharmacy.Kafka;
using Pharmacy.Persistence.Mongo;
using MediatR;
using Pharmacy.Handlers.EventHandlers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pharmacy.EventStore;
using Pharmacy.Handlers;

namespace Pharmacy.Web.API
{
    public static class InfrastructureRegistry
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            var eventstoreConnStr = config.GetConnectionString("eventstore");
            var producerConfig = new EventsProducerConfig(config.GetConnectionString("kafka"), config["eventsTopicName"]);

            var mongoConnStr = config.GetConnectionString("mongo");
            var mongoQueryDbName = config["queryDbName"];
            var mongoConfig = new MongoConfig(mongoConnStr, mongoQueryDbName);
            
            return services.Scan(scan =>
            {
                scan.FromAssembliesOf(typeof(PharmacyDataEventsHandler))
                    .RegisterHandlers(typeof(IRequestHandler<>))
                    .RegisterHandlers(typeof(IRequestHandler<,>))
                    .RegisterHandlers(typeof(INotificationHandler<>));
            })
                .AddMongoDb(mongoConfig)
                .AddKafkaEventProducer<Domain.Pharmacy, Guid>(producerConfig)
                .AddKafkaEventProducer<Domain.Medication, Guid>(producerConfig)
                .AddEventStore(eventstoreConnStr);
        }
    }
}
