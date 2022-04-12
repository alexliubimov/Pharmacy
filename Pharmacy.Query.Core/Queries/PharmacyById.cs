using MediatR;
using Pharmacy.Data;

namespace Pharmacy.Query.Core.Queries
{
    public record PharmacyById(Guid PharmacyId) : IRequest<PharmacyDetails>;
}
