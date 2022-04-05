using Pharmacy.Common.Models;

namespace Pharmacy.Domain.DomainEvents;

public static class MedicationEvents
{
    public record MedicationCreated : BaseDomainEvent<Medication, Guid>
    {
        private MedicationCreated()
        {

        }

        public MedicationCreated(Medication medication, Pharmacy pharmacy) : base(medication)
        {

        }

    }

    public record Dispense : BaseDomainEvent<Medication, Guid>
    {

    }

    public record Refill : BaseDomainEvent<Medication, Guid>
    {

    }
}