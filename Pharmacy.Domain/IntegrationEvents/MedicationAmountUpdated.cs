using MediatR;
using Pharmacy.Common.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Domain.IntegrationEvents
{
    public record MedicationAmountUpdated : IIntegrationEvent, INotification
    {
        public Guid Id { get; init; }
        public Guid MedicationId { get; init; }

        public MedicationAmountUpdated(Guid id, Guid medicationId)
        {
            Id = id;
            MedicationId = medicationId;
        }
    }
}
