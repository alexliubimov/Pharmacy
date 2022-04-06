using System.Runtime.InteropServices;
using Pharmacy.Common.Models;
using Pharmacy.Domain.DomainEvents;

namespace Pharmacy.Domain;

public record Pharmacy : BaseAggregateRoot<Pharmacy, Guid>
{
    private HashSet<Guid> _medicationIds = new();

    public string Name { get; private set; }
    public string Address { get; private set; }
    public IReadOnlyCollection<Guid> Medications => _medicationIds;

    private Pharmacy()
    {

    }

    public Pharmacy(Guid pharmacyId, string name, string address) : base(pharmacyId)
    {
        this.Append(new PharmacyEvents.PharmacyCreated(this, name, address));
    }

    public static Pharmacy Create(Guid pharmacyId, string name, string address)
    {
        return new Pharmacy(pharmacyId, name, address);
    }

    public void AddMedication(Guid medicationId)
    {
        this.Append(new PharmacyEvents.MedicationAdded(this, medicationId));

    }
    
    protected override void When(IDomainEvent<Guid> @event)
    {
        switch (@event)
        {
            case PharmacyEvents.PharmacyCreated c:
                this.Id = c.AggregateId;
                this.Name = c.Name;
                this.Address = c.Address;
                break;
            case PharmacyEvents.MedicationAdded m:
                _medicationIds.Add(m.MedicationId);
                break;
        }           

    }
}