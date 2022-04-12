using MediatR;
using Pharmacy.Data;
using Pharmacy.Persistence.Mongo;
using Pharmacy.Query.Core.Queries;
using MongoDB.Driver;

namespace Pharmacy.Handlers.QueryHandlers
{
    public class MedicationListByPharmacyIdHandler : IRequestHandler<MedicationListByPharmacyId, IEnumerable<MedicationDetails>>
    {
        private readonly IQueryDbContext _db;

        public MedicationListByPharmacyIdHandler(IQueryDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<MedicationDetails>> Handle(MedicationListByPharmacyId request, CancellationToken cancellationToken)
        {
            var cursor = await _db.Medications.FindAsync(c => c.PharmacyId == request.PharmacyId,
                null, cancellationToken);
            return await cursor.ToListAsync(cancellationToken);
        }
    }
}
