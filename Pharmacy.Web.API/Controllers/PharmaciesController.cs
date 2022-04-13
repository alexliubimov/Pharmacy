using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Query.Core.Queries;
using Pharmacy.Domain.Commands.CreatePharmacy;
using Pharmacy.Web.API.DTOs;

namespace Pharmacy.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PharmaciesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PharmaciesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetPharmacies()
        {
            var query = new PharmaciesList();

            var result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("{pharmacyId}")]
        public async Task<IActionResult> GetPharmacyByIdAsync(Guid pharmacyId)
        {
            var query = new PharmacyById(pharmacyId);

            var result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("{pharmacyId}/medications")]
        public async Task<IActionResult> GetMedicationsByPharmacyIdAsync(Guid pharmacyId)
        {
            var query = new MedicationListByPharmacyId(pharmacyId);

            var result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePharmacyAsync([FromBody] CreatePharmacyDto dto, CancellationToken cancellationToken = default)
        {
            if (dto == null)
            {
                return BadRequest();
            }

            var command = new CreatePharmacyCommand(Guid.NewGuid(), dto.Name, dto.Address);
            await _mediator.Publish(command, cancellationToken);

            return Ok(command.Id);
        }
    }
}
