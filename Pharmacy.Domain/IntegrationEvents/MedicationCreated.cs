using MediatR;
using Pharmacy.Common.Events;

namespace Pharmacy.Domain.IntegrationEvents
{
    public record MedicationCreated : IIntegrationEvent, INotification
    {
        public Guid Id { get; init; }
        public Guid MedicationId { get; init; }

        public MedicationCreated(Guid id, Guid medicationId)
        {
            Id = id;
            MedicationId = medicationId;
        }
    }
}
