using System.Runtime.InteropServices;
using Pharmacy.Common.Models;

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

    public Pharmacy(Guid pharmacyId, string name, string address)
    {

    }

    public static void Create(Guid pharmacyId, string name, string address)
    {

    }

    public void AddMedication(Guid medicationId)
    {

    }
    
    protected override void When(IDomainEvent<Guid> @event)
    {
        
    }
}