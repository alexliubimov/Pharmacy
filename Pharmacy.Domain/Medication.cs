using Pharmacy.Common.Models;

namespace Pharmacy.Domain;

public record Medication : BaseAggregateRoot<Medication, Guid>
{
    public string Name { get; private set; }
    public int PacksCount { get; private set; }
    public int PackSize { get; private set; }
    public Pharmacy Pharmacy { get; private set; }

    private Medication()
    {

    }

    public Medication(Pharmacy pharmacy, string name, int packSize, int packsCount)
    {

    }

    public static void Create(Guid medicationId, Pharmacy pharmacy, string name, int packSize, int packsCount)
    {

    }

    public void Dispense(int packsAmount)
    {

    }

    public void Refill(int packsAmount)
    {

    }

    protected override void When(IDomainEvent<Guid> @event)
    {
        
    }
}