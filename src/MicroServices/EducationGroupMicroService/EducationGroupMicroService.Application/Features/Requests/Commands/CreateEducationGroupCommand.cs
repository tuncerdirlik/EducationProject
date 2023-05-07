using EducationGroupMicroService.Application.DTOs;
using MediatR;

namespace EducationGroupMicroService.Application.Features.Requests.Commands
{
    public class CreateEducationGroupCommand : IRequest<BaseResponse<CreateEducationGroupDto>>
    {
        public CreateEducationGroupDto CreateEducationGroupDto { get; set; }
    }
}
