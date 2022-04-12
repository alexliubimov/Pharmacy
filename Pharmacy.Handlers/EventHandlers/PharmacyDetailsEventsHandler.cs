using MediatR;
using Pharmacy.Common;
using Pharmacy.Domain.IntegrationEvents;
using Pharmacy.Persistence.Mongo;
using Pharmacy.Data;
using MongoDB.Driver;

namespace Pharmacy.Handlers.EventHandlers
{
    public class PharmacyDetailsEventsHandler :
        INotificationHandler<PharmacyCreated>,
        INotificationHandler<MedicationCreated>,
        INotificationHandler<MedicationAmountUpdated>
    {
        private readonly IQueryDbContext _db;
        private readonly IAggregateRepository<Domain.Pharmacy, Guid> _pharmacyRepo;
        private readonly IAggregateRepository<Domain.Medication, Guid> _medicationRepo;

        public PharmacyDetailsEventsHandler(IQueryDbContext db, 
            IAggregateRepository<Domain.Pharmacy, Guid> pharmacyRepo, 
            IAggregateRepository<Domain.Medication, Guid> medicationRepo)
        {
            _db = db;
            _pharmacyRepo = pharmacyRepo;
            _medicationRepo = medicationRepo;
        }

        public async Task Handle(PharmacyCreated notification, CancellationToken cancellationToken)
        {
            await RehydrateAndSavePharmacyDetailsViewAsync(notification.PharmacyId, cancellationToken);
        }

        public async Task Handle(MedicationCreated notification, CancellationToken cancellationToken)
        {
            var medication = await _medicationRepo.RehydrateAsync(notification.MedicationId, cancellationToken);

            await RehydrateAndSavePharmacyDetailsViewAsync(medication.PharmacyId, cancellationToken);
        }

        public async Task Handle(MedicationAmountUpdated notification, CancellationToken cancellationToken)
        {
            var medication = await _medicationRepo.RehydrateAsync(notification.MedicationId, cancellationToken);

            await RehydrateAndSavePharmacyDetailsViewAsync(medication.PharmacyId, cancellationToken);
        }

        private async Task RehydrateAndSavePharmacyDetailsViewAsync(Guid pharmacyId, CancellationToken cancellationToken)
        {
            var customerView = await BuildPharmacyDetailsViewAsync(pharmacyId, cancellationToken);

            await SavePharmacyDetailsViewAsync(customerView, cancellationToken);
        }

        private async Task<PharmacyDetails> BuildPharmacyDetailsViewAsync(Guid pharmacyId, CancellationToken cancellationToken)
        {
            var pharmacy = await _pharmacyRepo.RehydrateAsync(pharmacyId, cancellationToken);

            var medications = new MedicationDetails[pharmacy.Medications.Count];

            int index = 0;
            foreach (var id in pharmacy.Medications)
            {
                var medication = await _medicationRepo.RehydrateAsync(id, cancellationToken);
                medications[index++] = new MedicationDetails(medication.Id, medication.PharmacyId, medication.Name, medication.PacksCount, medication.PackSize, pharmacy.Name);
            }

            return new PharmacyDetails(pharmacy.Id, pharmacy.Name, pharmacy.Address, medications);
        }

        private async Task SavePharmacyDetailsViewAsync(PharmacyDetails pharmacyView, CancellationToken cancellationToken)
        {
            var filter = Builders<PharmacyDetails>.Filter
                            .Eq(a => a.Id, pharmacyView.Id);

            var update = Builders<PharmacyDetails>.Update
                .Set(a => a.Id, pharmacyView.Id)
                .Set(a => a.Name, pharmacyView.Name)
                .Set(a => a.Address, pharmacyView.Address)
                .Set(a => a.Medications, pharmacyView.Medications);

            await _db.PharmaciesDetails.UpdateOneAsync(filter,
                cancellationToken: cancellationToken,
                update: update,
                options: new UpdateOptions() { IsUpsert = true });
        }
    }
}
