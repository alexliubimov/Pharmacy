using MediatR;
using Pharmacy.Data;

namespace Pharmacy.Query.Core.Queries
{
    public record MedicationById(Guid PharmacyId, Guid MedicationId) : IRequest<MedicationDetails>;
}
