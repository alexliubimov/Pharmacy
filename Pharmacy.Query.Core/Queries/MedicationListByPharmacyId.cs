using MediatR;
using Pharmacy.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Query.Core.Queries
{
    public record MedicationListByPharmacyId(Guid PharmacyId) : IRequest<IEnumerable<MedicationDetails>>;
}
