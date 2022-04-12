using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Data
{
    public record PharmacyData
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string Address { get; init; }

        public PharmacyData(Guid id, string name, string adress)
        {
            Id = id;
            Name = name;
            Address = adress;
        }
    }
}
