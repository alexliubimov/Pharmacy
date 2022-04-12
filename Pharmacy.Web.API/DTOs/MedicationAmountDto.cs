using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Web.API.DTOs
{
    public class MedicationAmountDto
    {
        [Required, Range(0, int.MaxValue)]
        public int PacksCount { get; set; }
    }
}
