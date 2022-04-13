using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pharmacy.Common.Events;
using Pharmacy.Common.Serialization;
using Pharmacy.Domain.DomainEvents;
using Pharmacy.Events.Worker;
using Pharmacy.EventStore;
using Pharmacy.Handlers;
using Pharmacy.Handlers.EventHandlers;
using Pharmacy.Kafka;
using Pharmacy.Persistence.Mongo;

await Host.CreateDefaultBuilder(args)
    .ConfigureHostConfiguration(configurationBuilder =>
    {
        configurationBuilder.AddCommandLine(args);
    })
    .ConfigureAppConfiguration((ctx, builder) =>
    {
        builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{ctx.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();
    })
    .ConfigureServices((hostContext, services) =>
    {
        var kafkaConnStr = hostContext.Configuration.GetConnectionString("kafka");
        var eventsTopicName = hostContext.Configuration["eventsTopicName"];
        var groupName = hostContext.Configuration["eventsTopicGroupName"];
        var consumerConfig = new EventsConsumerConfig(kafkaConnStr, eventsTopicName, groupName);

        var mongoConnStr = hostContext.Configuration.GetConnectionString("mongo");
        var mongoQueryDbName = hostContext.Configuration["queryDbName"];
        var mongoConfig = new MongoConfig(mongoConnStr, mongoQueryDbName);

        var eventstoreConnStr = hostContext.Configuration.GetConnectionString("eventstore");

        services.Scan(scan =>
        {
            scan.FromAssembliesOf(typeof(PharmacyDataEventsHandler))
                .RegisterHandlers(typeof(INotificationHandler<>));
        })
            .AddScoped<ServiceFactory>(ctx => ctx.GetRequiredService)
            .AddScoped<IMediator, Mediator>()
            .AddSingleton<IEventSerializer>(new JsonEventSerializer(new[]
            {
                typeof(PharmacyEvents.PharmacyCreated).Assembly
            }))
            .AddSingleton(consumerConfig)
            .AddSingleton(typeof(IEventConsumer), typeof(EventConsumer))
            .AddMongoDb(mongoConfig)
            .AddEventStore(eventstoreConnStr)
            .AddHostedService<EventsConsumerWorker>(); ;
    })
    .Build()
    .RunAsync();