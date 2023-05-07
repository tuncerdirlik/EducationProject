using EducationGroupMicroService.Application.DTOs;
using EducationGroupMicroService.Application.Features.Requests.Commands;
using EducationGroupMicroService.Application.Features.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EducationGroupMicroService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EducationGroupController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EducationGroupController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EducationGroupDto>> Get(int id)
        {
            var items = await _mediator.Send(new GetEducationGroupItemRequest { Id = id });
            return Ok(items);
        }

        [HttpGet]
        public async Task<ActionResult<List<EducationGroupDto>>> Get()
        {
            var items = await _mediator.Send(new GetEducationGroupListRequest {});
            return Ok(items);
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<CreateEducationGroupDto>>> Post([FromBody] CreateEducationGroupDto educationGroup)
        {
            var command = new CreateEducationGroupCommand { CreateEducationGroupDto = educationGroup };
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] EducationGroupDto educationGroup)
        {
            var command = new UpdateEducationGroupCommand { EducationGroupDto = educationGroup };
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResponse<CreateEducationGroupDto>>> Delete(int id)
        {
            var command = new DeleteEducationGroupCommand { Id = id };
            var response = await _mediator.Send(command);

            return Ok(response);
        }

    }
}
