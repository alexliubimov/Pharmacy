using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Domain.Commands.CreatePharmacy
{
    public record CreatePharmacyCommand : INotification
    {
        public string Name { get; init; }
        public string Address { get; init; }

        public CreatePharmacyCommand(string name, string address)
        {
            Name = name;
            Address = address;
        }
    }
}
