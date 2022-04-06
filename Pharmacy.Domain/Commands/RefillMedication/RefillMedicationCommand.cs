using MediatR;

namespace Pharmacy.Domain.Commands.RefillMedication
{
    public record RefillMedicationCommand : INotification
    {
        public Guid MedicationId { get; init; }
        public int PacksAmount { get; init; }

        public RefillMedicationCommand(Guid medicationId, int packsAmount)
        {
            MedicationId = medicationId;
            PacksAmount = packsAmount;
        }
    }
}
