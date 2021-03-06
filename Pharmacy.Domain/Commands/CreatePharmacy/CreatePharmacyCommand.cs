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
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string Address { get; init; }

        public CreatePharmacyCommand(Guid id, string name, string address)
        {
            Id = id;
            Name = name;
            Address = address;
        }
    }
}
