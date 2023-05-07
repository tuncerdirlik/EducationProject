using EducationGroupMicroService.Application.DTOs;
using MediatR;

namespace EducationGroupMicroService.Application.Features.Requests.Commands
{
    public class DeleteEducationGroupCommand : IRequest<BaseResponse<EducationGroupDto>>
    {
        public int Id { get; set; }
    }
}
