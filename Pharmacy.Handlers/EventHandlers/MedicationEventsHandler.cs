using MediatR;
using MongoDB.Driver;
using Pharmacy.Common;
using Pharmacy.Data;
using Pharmacy.Domain.IntegrationEvents;
using Pharmacy.Persistence.Mongo;

namespace Pharmacy.Handlers.EventHandlers
{
    public class MedicationEventsHandler : 
        INotificationHandler<MedicationCreated>,
        INotificationHandler<MedicationAmountUpdated>
    {
        private readonly IQueryDbContext _db;
        private readonly IAggregateRepository<Domain.Pharmacy, Guid> _pharmacyRepo;
        private readonly IAggregateRepository<Domain.Medication, Guid> _medicationRepo;

        public MedicationEventsHandler(IQueryDbContext db,
            IAggregateRepository<Domain.Pharmacy, Guid> pharmacyRepo,
            IAggregateRepository<Domain.Medication, Guid> medicationRepo)
        {
            _db = db;
            _pharmacyRepo = pharmacyRepo;
            _medicationRepo = medicationRepo;
        }

        public async Task Handle(MedicationCreated notification, CancellationToken cancellationToken)
        {
            await RehydrateAndSaveMedicationViewAsync(notification.Id, cancellationToken);
        }

        public async Task Handle(MedicationAmountUpdated notification, CancellationToken cancellationToken)
        {
            await RehydrateAndSaveMedicationViewAsync(notification.Id, cancellationToken);
        }

        private async Task RehydrateAndSaveMedicationViewAsync(Guid medicationId, CancellationToken cancellationToken)
        {
            var medicationView = await BuildMedicationViewAsync(medicationId, cancellationToken);

            await SaveMedicationViewAsync(medicationView, cancellationToken);
        }

        private async Task<MedicationDetails> BuildMedicationViewAsync(Guid medicationId, CancellationToken cancellationToken)
        {
            var medication = await _medicationRepo.RehydrateAsync(medicationId, cancellationToken);
            var pharmacy = await _pharmacyRepo.RehydrateAsync(medication.PharmacyId, cancellationToken);

            return new MedicationDetails(medication.Id, medication.PharmacyId, medication.Name, medication.PacksCount, medication.PackSize, pharmacy.Name);
        }

        private async Task SaveMedicationViewAsync(MedicationDetails medicationView, CancellationToken cancellationToken)
        {
            var filter = Builders<MedicationDetails>.Filter
                            .Eq(m => m.Id, medicationView.Id);

            var update = Builders<MedicationDetails>.Update
                .Set(m => m.Id, medicationView.Id)
                .Set(m => m.Name, medicationView.Name)
                .Set(m => m.PacksCount, medicationView.PacksCount)
                .Set(m => m.PackSize, medicationView.PackSize)
                .Set(m => m.PharmacyId, medicationView.PharmacyId)
                .Set(m => m.PharmacyName, medicationView.PharmacyName);


            await _db.Medications.UpdateOneAsync(filter,
                cancellationToken: cancellationToken,
                update: update,
                options: new UpdateOptions() { IsUpsert = true });
        }
    }
}
