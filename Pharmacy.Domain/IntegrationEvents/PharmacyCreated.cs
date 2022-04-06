using MediatR;
using Pharmacy.Common.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Domain.IntegrationEvents
{
    public record PharmacyCreated : IIntegrationEvent, INotification
    {
        public Guid Id { get; init; }
        public Guid PharmacyId { get; init; }

        public PharmacyCreated(Guid id, Guid pharmacyId)
        {
            Id = id;
            PharmacyId = pharmacyId;
        }
    }
}
