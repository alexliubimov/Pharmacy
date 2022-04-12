namespace Pharmacy.Web.API.DTOs
{
    public class CreateMedicationDto
    {
        public string Name { get; set; }
        public int PackSize { get; set; }
        public int PacksCount { get; set; }
    }
}
