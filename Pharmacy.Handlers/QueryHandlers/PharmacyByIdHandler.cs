using MediatR;
using Pharmacy.Data;
using Pharmacy.Persistence.Mongo;
using Pharmacy.Query.Core.Queries;
using MongoDB.Driver;

namespace Pharmacy.Handlers.QueryHandlers
{
    public class PharmacyByIdHandler : IRequestHandler<PharmacyById, PharmacyDetails>
    {
        private readonly IQueryDbContext _db;

        public PharmacyByIdHandler(IQueryDbContext db)
        {
            _db = db;
        }

        public async Task<PharmacyDetails> Handle(PharmacyById request, CancellationToken cancellationToken)
        {
            var cursor = await _db.PharmaciesDetails.FindAsync(c => c.Id == request.PharmacyId,
                 null, cancellationToken);
            return await cursor.FirstOrDefaultAsync(cancellationToken);
        }
    }
}
