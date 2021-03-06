using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Domain.Commands.CreateMedication
{
    public record CreateMedicationCommand : INotification
    {
        public Guid Id { get; init; }
        public Guid PharmacyId { get; init; }
        public string Name { get; init; }
        public int PackSize { get; init; }
        public int PacksCount { get; init; }

        public CreateMedicationCommand(Guid id, Guid pharmacyId, string name, int packSize, int packsCount)
        {
            Id = id;
            PharmacyId = pharmacyId;
            Name = name;
            PackSize = packSize;
            PacksCount = packsCount;
        }
    }
}
