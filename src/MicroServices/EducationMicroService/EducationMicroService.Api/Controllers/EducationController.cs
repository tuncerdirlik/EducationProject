using EducationMicroService.Application.DTOs;
using EducationMicroService.Application.Features.Requests.Commands;
using EducationMicroService.Application.Features.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EducationMicroService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EducationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EducationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EducationDto>> Get(int id)
        {
            var items = await _mediator.Send(new GetEducationItemRequest { Id = id });
            return Ok(items);
        }

        [HttpGet]
        public async Task<ActionResult<List<EducationDto>>> Get()
        {
            var items = await _mediator.Send(new GetEducationListRequest { });
            return Ok(items);
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<CreateEducationDto>>> Post([FromBody] CreateEducationDto educationGroup)
        {
            var command = new CreateEducationCommand { CreateEducationDto = educationGroup };
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] EducationDto educationGroup)
        {
            var command = new UpdateEducationCommand { EducationDto = educationGroup };
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResponse<CreateEducationDto>>> Delete(int id)
        {
            var command = new DeleteEducationCommand { Id = id };
            var response = await _mediator.Send(command);

            return Ok(response);
        }
    }
}
