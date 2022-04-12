using MediatR;
using MongoDB.Driver;
using Pharmacy.Data;
using Pharmacy.Persistence.Mongo;
using Pharmacy.Query.Core.Queries;

namespace Pharmacy.Handlers.QueryHandlers
{
    public class MedicationByIdHandler : IRequestHandler<MedicationById, MedicationDetails>
    {
        private readonly IQueryDbContext _db;

        public MedicationByIdHandler(IQueryDbContext db)
        {
            _db = db;
        }

        public async Task<MedicationDetails> Handle(MedicationById request, CancellationToken cancellationToken)
        {
            var cursor = await _db.Medications.FindAsync(c => c.Id == request.MedicationId && c.PharmacyId == request.PharmacyId,
                null, cancellationToken);
            return await cursor.FirstOrDefaultAsync(cancellationToken);
        }
    }
}
