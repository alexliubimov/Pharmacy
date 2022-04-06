using MediatR;
using Pharmacy.Common;
using Pharmacy.Common.Events;
using Pharmacy.Domain.IntegrationEvents;

namespace Pharmacy.Domain.Commands.RefillMedication
{
    public class RefillMedicationCommandHandler : INotificationHandler<RefillMedicationCommand>
    {
        private readonly IAggregateRepository<Medication, Guid> _medicationEventsService;
        private readonly IEventProducer _eventProducer;

        public RefillMedicationCommandHandler(IAggregateRepository<Medication, Guid> medicationEventsService, IEventProducer eventProducer)
        {
            _medicationEventsService = medicationEventsService;
            _eventProducer = eventProducer;
        }

        public async Task Handle(RefillMedicationCommand notification, CancellationToken cancellationToken)
        {
            var medication = await _medicationEventsService.RehydrateAsync(notification.MedicationId);

            medication.Refill(notification.PacksAmount);

            var @event = new MedicationAmountUpdated(Guid.NewGuid(), notification.MedicationId);
            await _eventProducer.DispatchAsync(@event, cancellationToken);
        }
    }
}
