using MediatR;
using Pharmacy.Common;
using Pharmacy.Common.Events;
using Pharmacy.Domain.IntegrationEvents;

namespace Pharmacy.Domain.Commands.CreateMedication
{
    public class CreateMedicationCommandHandler : INotificationHandler<CreateMedicationCommand>
    {
        private readonly IAggregateRepository<Medication, Guid> _medicationEventsService;
        private readonly IAggregateRepository<Pharmacy, Guid> _pharmacyEventsService;
        private readonly IEventProducer _eventProducer;

        public CreateMedicationCommandHandler(IAggregateRepository<Medication, Guid> medicationEventsService, IAggregateRepository<Pharmacy, Guid> pharmacyEventsService, IEventProducer eventProducer)
        {
            _medicationEventsService = medicationEventsService;
            _pharmacyEventsService = pharmacyEventsService;
            _eventProducer = eventProducer;
        }

        public async Task Handle(CreateMedicationCommand notification, CancellationToken cancellationToken)
        {
            var pharmacy = await _pharmacyEventsService.RehydrateAsync(notification.PharmacyId);

            if (pharmacy == null)
            {
                throw new ArgumentOutOfRangeException(nameof(CreateMedicationCommand.PharmacyId), "invalid pharmacy id");
            }

            var medication = Medication.Create(notification.Id, pharmacy, notification.Name, notification.PackSize, notification.PacksCount);

            await _pharmacyEventsService.PersistAsync(pharmacy);
            await _medicationEventsService.PersistAsync(medication);

            var med = await _medicationEventsService.RehydrateAsync(medication.Id);

            var @event = new MedicationCreated(Guid.NewGuid(), medication.Id);
            await _eventProducer.DispatchAsync(@event, cancellationToken);
        }
    }
}
