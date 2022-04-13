using MediatR;
using Pharmacy.Common;
using Pharmacy.Common.Events;
using Pharmacy.Domain.IntegrationEvents;

namespace Pharmacy.Domain.Commands.DispenseMedication
{
    public class DispenseMedicationCommandHandler : INotificationHandler<DispenseMedicationCommand>
    {
        private readonly IAggregateRepository<Medication, Guid> _medicationEventsService;
        private readonly IEventProducer _eventProducer;

        public DispenseMedicationCommandHandler(IAggregateRepository<Medication, Guid> medicationEventsService, IEventProducer eventProducer)
        {
            _medicationEventsService = medicationEventsService;
            _eventProducer = eventProducer;
        }

        public async Task Handle(DispenseMedicationCommand notification, CancellationToken cancellationToken)
        {
            var medication = await _medicationEventsService.RehydrateAsync(notification.MedicationId);

            medication.Dispense(notification.PacksAmount);

            await _medicationEventsService.PersistAsync(medication);

            var @event = new MedicationAmountUpdated(Guid.NewGuid(), notification.MedicationId);
            await _eventProducer.DispatchAsync(@event, cancellationToken);
        }
    }
}
