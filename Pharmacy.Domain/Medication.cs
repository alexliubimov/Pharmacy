using Pharmacy.Common.Models;
using Pharmacy.Domain.DomainEvents;

namespace Pharmacy.Domain;

public record Medication : BaseAggregateRoot<Medication, Guid>
{
    public string Name { get; private set; }
    public int PacksCount { get; private set; }
    public int PackSize { get; private set; }
    public Guid PharmacyId { get; private set; }

    private Medication()
    {

    }

    public Medication(Guid medicationId, Pharmacy pharmacy, string name, int packSize, int packsCount) : base(medicationId)
    {
        this.Append(new MedicationEvents.MedicationCreated(this, pharmacy, name, packSize, packsCount));
    }

    public static Medication Create(Guid medicationId, Pharmacy pharmacy, string name, int packSize, int packsCount)
    {
        var medication = new Medication(medicationId, pharmacy, name, packSize, packsCount);
        pharmacy.AddMedication(medicationId);

        return medication;
    }

    public void Dispense(int packsAmount)
    {
        this.Append(new MedicationEvents.Dispense(this, packsAmount));
    }

    public void Refill(int packsAmount)
    {
        this.Append(new MedicationEvents.Refill(this, packsAmount));
    }

    protected override void When(IDomainEvent<Guid> @event)
    {
        switch (@event)
        {
            case MedicationEvents.MedicationCreated c:
                this.Id = c.AggregateId;
                this.Name = c.Name;
                this.PharmacyId = c.PharmacyId;
                this.PackSize = c.PackSize;
                this.PacksCount = c.PacksCount;
                break;
            case MedicationEvents.Dispense d:
                this.PacksCount -= d.PacksAmount;
                break;
            case MedicationEvents.Refill r:
                this.PacksCount += r.PacksAmount;
                break;
        }
    }
}