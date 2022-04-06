using MediatR;
using Pharmacy.Common;
using Pharmacy.Common.Events;
using Pharmacy.Domain.IntegrationEvents;

namespace Pharmacy.Domain.Commands.CreatePharmacy
{
    public class CreatePharmacyCommandHandler : INotificationHandler<CreatePharmacyCommand>
    {
        private readonly IAggregateRepository<Pharmacy, Guid> _pharmacyEventsService;
        private readonly IEventProducer _eventProducer;

        public CreatePharmacyCommandHandler(IAggregateRepository<Pharmacy, Guid> pharmacyEventsService, IEventProducer eventProducer)
        {
            _pharmacyEventsService = pharmacyEventsService;
            _eventProducer = eventProducer;
        }

        public async Task Handle(CreatePharmacyCommand notification, CancellationToken cancellationToken)
        {
            var pharmacy = Pharmacy.Create(Guid.NewGuid(), notification.Name, notification.Address);

            await _pharmacyEventsService.PersistAsync(pharmacy);

            var @event = new PharmacyCreated(Guid.NewGuid(), pharmacy.Id);
            await _eventProducer.DispatchAsync(@event, cancellationToken);
        }
    }
}
