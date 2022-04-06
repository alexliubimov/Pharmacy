using Pharmacy.Common.Models;

namespace Pharmacy.Domain.DomainEvents;

public static class PharmacyEvents
{
    public record PharmacyCreated : BaseDomainEvent<Pharmacy, Guid>
    {
        public string Name { get; init; }
        public string Address { get; init; }

        private PharmacyCreated()
        {

        }

        public PharmacyCreated(Pharmacy pharmacy, string name, string address) : base(pharmacy)
        {
            Name = name;
            Address = address;
        }
    }

    public record MedicationAdded : BaseDomainEvent<Pharmacy, Guid>
    {
        public Guid MedicationId { get; init; }

        private MedicationAdded()
        {

        }

        public MedicationAdded(Pharmacy pharmacy, Guid medicationId) : base(pharmacy)
        {
            MedicationId = medicationId;
        } 
    }
}