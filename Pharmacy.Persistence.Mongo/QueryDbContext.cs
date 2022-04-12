using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Pharmacy.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Persistence.Mongo
{
    public class QueryDbContext : IQueryDbContext
    {
        private readonly IMongoDatabase _db;

        private static readonly IBsonSerializer guidSerializer = new GuidSerializer(GuidRepresentation.Standard);

        public IMongoCollection<PharmacyData> Pharmacies { get; }
        public IMongoCollection<PharmacyDetails> PharmaciesDetails { get; }
        public IMongoCollection<MedicationDetails> Medications { get; }

        static QueryDbContext()
        {
            RegisterMappings();
        }

        public QueryDbContext(IMongoDatabase db)
        {
            _db = db ?? throw new System.ArgumentNullException(nameof(db));

            Pharmacies = _db.GetCollection<PharmacyData>("pharmacies");
            PharmaciesDetails = _db.GetCollection<PharmacyDetails>("pharmaciesdetails");
            Medications = _db.GetCollection<MedicationDetails>("medications");
        }

        private static void RegisterMappings()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(PharmacyData)))
                BsonClassMap.RegisterClassMap<PharmacyData>(mapper =>
                {
                    mapper.MapIdProperty(c => c.Id).SetSerializer(guidSerializer);
                    mapper.MapProperty(c => c.Name);
                    mapper.MapProperty(c => c.Address);
                    mapper.MapCreator(c => new PharmacyData(c.Id, c.Name, c.Address));
                });

            if (!BsonClassMap.IsClassMapRegistered(typeof(PharmacyDetails)))
                BsonClassMap.RegisterClassMap<PharmacyDetails>(mapper =>
                {
                    mapper.MapIdProperty(c => c.Id).SetSerializer(guidSerializer);
                    mapper.MapProperty(c => c.Name);
                    mapper.MapProperty(c => c.Address);
                    mapper.MapProperty(c => c.Medications);
                    mapper.MapCreator(c => new PharmacyDetails(c.Id, c.Name, c.Address, c.Medications));
                });

            if (!BsonClassMap.IsClassMapRegistered(typeof(MedicationDetails)))
                BsonClassMap.RegisterClassMap<MedicationDetails>(mapper =>
                {
                    mapper.MapIdProperty(c => c.Id).SetSerializer(guidSerializer);
                    mapper.MapProperty(c => c.PharmacyId);
                    mapper.MapProperty(c => c.Name);
                    mapper.MapProperty(c => c.PacksCount);
                    mapper.MapProperty(c => c.PackSize);
                    mapper.MapProperty(c => c.PharmacyName);
                    mapper.MapCreator(c => new MedicationDetails(c.Id, c.PharmacyId, c.Name, c.PacksCount, c.PackSize, c.PharmacyName));
                });
        }
    }
}
