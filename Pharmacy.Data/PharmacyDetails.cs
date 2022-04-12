using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Data
{
    public record PharmacyDetails
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string Address { get; init; }
        public MedicationDetails[] Medications { get; init; }

        public PharmacyDetails(Guid id, string name, string address, MedicationDetails[] medications)
        {
            Id = id;
            Name = name;
            Address = address;
            Medications = medications; 
        }
    }
}
