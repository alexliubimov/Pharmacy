using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Pharmacy.Common.Events;
using Pharmacy.Common.Models;
namespace Pharmacy.Kafka
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddKafkaEventProducer<TA, TK>(this IServiceCollection services, EventsProducerConfig configuration)
            where TA : class, IAggregateRoot<TK>
        {
            return services.AddSingleton<IEventProducer>(ctx =>
            {
                var logger = ctx.GetRequiredService<ILogger<EventProducer>>();
                return new EventProducer(configuration.TopicName, configuration.KafkaConnectionString, logger);
            });
        }
    }
}
