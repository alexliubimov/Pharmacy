using Pharmacy.Common.Models;

namespace Pharmacy.Domain.DomainEvents;

public static class MedicationEvents
{
    public record MedicationCreated : BaseDomainEvent<Medication, Guid>
    {
        public string Name { get; init; }
        public int PackSize { get; init; }
        public int PacksCount { get; init; }
        public Guid PharmacyId { get; init; }

        private MedicationCreated()
        {

        }

        public MedicationCreated(Medication medication, Pharmacy pharmacy, string name, int packSize, int packsCount) : base(medication)
        {
            Name = name;
            PackSize = packSize;
            PacksCount = packsCount;
            PharmacyId = pharmacy.Id;
        }
    }

    public record Dispense : BaseDomainEvent<Medication, Guid>
    {
        public int PacksAmount { get; init; }

        private Dispense()
        {

        }

        public Dispense(Medication medication, int packsAmount) : base(medication)
        {
            PacksAmount = packsAmount;  
        }        
    }

    public record Refill : BaseDomainEvent<Medication, Guid>
    {
        public int PacksAmount { get; init; }

        private Refill()
        {

        }

        public Refill(Medication medication, int packsCount) : base(medication)
        {
            PacksAmount = packsCount;
        }
    }
}