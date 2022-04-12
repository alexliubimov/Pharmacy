using MediatR;
using Pharmacy.Data;

namespace Pharmacy.Query.Core.Queries
{
    public record PharmaciesList : IRequest<IEnumerable<PharmacyData>>;
}
