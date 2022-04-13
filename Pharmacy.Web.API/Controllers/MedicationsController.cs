using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Domain.Commands.CreateMedication;
using Pharmacy.Domain.Commands.DispenseMedication;
using Pharmacy.Domain.Commands.RefillMedication;
using Pharmacy.Query.Core.Queries;
using Pharmacy.Web.API.DTOs;

namespace Pharmacy.Web.API.Controllers
{
    [Route("api/pharmacies/{pharmacyId}/[controller]")]
    [ApiController]
    public class MedicationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MedicationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{medicationId}")]
        public async Task<IActionResult> GetMedicationByIdAsync(Guid pharmacyId, Guid medicationId)
        {
            var query = new MedicationById(pharmacyId, medicationId);

            var result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMedicationAsync(Guid pharmacyId, [FromBody] CreateMedicationDto dto, CancellationToken cancellationToken = default)
        {
            if (dto == null)
            {
                return BadRequest();
            }

            var command = new CreateMedicationCommand(Guid.NewGuid(), pharmacyId, dto.Name, dto.PackSize, dto.PacksCount);
            await _mediator.Publish(command, cancellationToken);

            return Ok(command.Id);
        }

        [HttpPatch("{medicationId}/dispense")]
        public async Task<IActionResult> DispenseMedicationAsync(Guid pharmacyId, Guid medicationId, [FromBody] MedicationAmountDto dto, CancellationToken cancellationToken = default)
        {
            if (dto == null)
            {
                return BadRequest();
            }

            var command = new DispenseMedicationCommand(medicationId, dto.PacksCount);
            await _mediator.Publish(command, cancellationToken);

            return Ok();
        }

        [HttpPatch("{medicationId}/refill")]
        public async Task<IActionResult> RefillMedicationAsync(Guid pharmacyId, Guid medicationId, [FromBody] MedicationAmountDto dto, CancellationToken cancellationToken = default)
        {
            if (dto == null)
            {
                return BadRequest();
            }

            var command = new RefillMedicationCommand(medicationId, dto.PacksCount);
            await _mediator.Publish(command, cancellationToken);

            return Ok();
        }
    }
}
