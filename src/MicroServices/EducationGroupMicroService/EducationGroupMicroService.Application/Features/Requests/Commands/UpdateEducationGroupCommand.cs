using EducationGroupMicroService.Application.DTOs;
using MediatR;

namespace EducationGroupMicroService.Application.Features.Requests.Commands
{
    public class UpdateEducationGroupCommand : IRequest<BaseResponse<EducationGroupDto>>
    {
        public EducationGroupDto EducationGroupDto { get; set; }
    }
}
