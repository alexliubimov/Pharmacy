using MongoDB.Driver;
using Pharmacy.Data;

namespace Pharmacy.Persistence.Mongo
{
    public interface IQueryDbContext
    {
        IMongoCollection<PharmacyDetails> PharmaciesDetails { get; }
        IMongoCollection<PharmacyData> Pharmacies { get; }
        IMongoCollection<MedicationDetails> Medications { get; }
    }
}
