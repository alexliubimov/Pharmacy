using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Pharmacy.Common;
using Pharmacy.Common.Models;
using Pharmacy.Common.Serialization;
using Pharmacy.Domain;

namespace Pharmacy.EventStore
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddEventStore(this IServiceCollection services, string connectionString)
        {
            return services.AddSingleton<IEventStoreConnectionWrapper>(ctx =>
            {
                var logger = ctx.GetRequiredService<ILogger<EventStoreConnectionWrapper>>();
                return new EventStoreConnectionWrapper(new Uri(connectionString), logger);
            }).AddEventsRepository<Domain.Pharmacy, Guid>()
                .AddEventsRepository<Medication, Guid>();
        }

        private static IServiceCollection AddEventsRepository<TA, TK>(this IServiceCollection services)
            where TA : class, IAggregateRoot<TK>
        {
            return services.AddSingleton<IAggregateRepository<TA, TK>>(ctx =>
            {
                var connectionWrapper = ctx.GetRequiredService<IEventStoreConnectionWrapper>();
                var eventDeserializer = ctx.GetRequiredService<IEventSerializer>();
                return new AggregateRepository<TA, TK>(connectionWrapper, eventDeserializer);
            });
        }
    }
}
