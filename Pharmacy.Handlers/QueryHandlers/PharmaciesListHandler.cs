using MediatR;
using Pharmacy.Data;
using Pharmacy.Persistence.Mongo;
using Pharmacy.Query.Core.Queries;
using MongoDB.Driver;

namespace Pharmacy.Handlers.QueryHandlers
{
    public class PharmaciesListHandler : IRequestHandler<PharmaciesList, IEnumerable<PharmacyData>>
    {
        private readonly IQueryDbContext _db;

        public PharmaciesListHandler(IQueryDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<PharmacyData>> Handle(PharmaciesList request, CancellationToken cancellationToken)
        {
            var filter = Builders<PharmacyData>.Filter.Empty;
            var cursor = await _db.Pharmacies.FindAsync(filter, null, cancellationToken);

            return await cursor.ToListAsync(cancellationToken);
        }
    }
}
