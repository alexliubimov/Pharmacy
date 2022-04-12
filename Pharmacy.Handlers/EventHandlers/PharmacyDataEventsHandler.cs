using MediatR;
using Pharmacy.Persistence.Mongo;
using Pharmacy.Domain.IntegrationEvents;
using Pharmacy.Common;
using Pharmacy.Data;
using MongoDB.Driver;

namespace Pharmacy.Handlers.EventHandlers
{
    public class PharmacyDataEventsHandler :
        INotificationHandler<PharmacyCreated>
    {
        private readonly IQueryDbContext _db;
        private readonly IAggregateRepository<Domain.Pharmacy, Guid> _pharmacyRepo;

        public PharmacyDataEventsHandler(IQueryDbContext db, IAggregateRepository<Domain.Pharmacy, Guid> pharmacyRepo)
        {
            _db = db;
            _pharmacyRepo = pharmacyRepo;
        }

        public async Task Handle(PharmacyCreated notification, CancellationToken cancellationToken)
        {
            await RehydrateAndSavePharmacyDetailsViewAsync(notification.PharmacyId, cancellationToken);
        }

        private async Task RehydrateAndSavePharmacyDetailsViewAsync(Guid pharmacyId, CancellationToken cancellationToken)
        {
            var customerView = await BuildPharmacyDataViewAsync(pharmacyId, cancellationToken);

            await SavePharmacyDataViewAsync(customerView, cancellationToken);
        }

        private async Task<PharmacyData> BuildPharmacyDataViewAsync(Guid pharmacyId, CancellationToken cancellationToken)
        {
            var pharmacy = await _pharmacyRepo.RehydrateAsync(pharmacyId, cancellationToken);

            return new PharmacyData(pharmacy.Id, pharmacy.Name, pharmacy.Address);
        }

        private async Task SavePharmacyDataViewAsync(PharmacyData pharmacyView, CancellationToken cancellationToken)
        {
            var filter = Builders<PharmacyData>.Filter
                            .Eq(a => a.Id, pharmacyView.Id);

            var update = Builders<PharmacyData>.Update
                .Set(a => a.Id, pharmacyView.Id)
                .Set(a => a.Name, pharmacyView.Name)
                .Set(a => a.Address, pharmacyView.Address);

            await _db.Pharmacies.UpdateOneAsync(filter,
                cancellationToken: cancellationToken,
                update: update,
                options: new UpdateOptions() { IsUpsert = true });
        }
    }
}
