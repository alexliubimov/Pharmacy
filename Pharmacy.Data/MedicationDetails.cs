using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Data
{
    public record MedicationDetails
    {
        public Guid Id { get; init; }
        public Guid PharmacyId { get; init; }
        public string Name { get; init; }
        public int PacksCount { get; init; }
        public int PackSize { get; init; }
        public string PharmacyName { get; init; }

        public MedicationDetails(Guid id, Guid pharmacyId, string name, int packsCount, int packSize, string pharmacyName)
        {
            Id = id;
            PharmacyId = pharmacyId;
            Name = name;
            PacksCount = packsCount;
            PackSize = packSize;
            PharmacyName = pharmacyName;
        }
    }
}
