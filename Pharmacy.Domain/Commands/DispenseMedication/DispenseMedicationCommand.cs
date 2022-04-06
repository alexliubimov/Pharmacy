using MediatR;

namespace Pharmacy.Domain.Commands.DispenseMedication
{
    public record DispenseMedicationCommand : INotification
    {
        public Guid MedicationId { get; init; }
        public int PacksAmount { get; init; }

        public DispenseMedicationCommand(Guid medicationId, int packsAmount)
        {
            MedicationId = medicationId;
            PacksAmount = packsAmount;
        }
    }
}
