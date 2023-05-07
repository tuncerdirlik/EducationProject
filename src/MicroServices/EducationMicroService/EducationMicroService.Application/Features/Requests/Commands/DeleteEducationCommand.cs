using EducationMicroService.Application.DTOs;
using MediatR;

namespace EducationMicroService.Application.Features.Requests.Commands
{
    public class DeleteEducationCommand : IRequest<BaseResponse<EducationDto>>
    {
        public int Id { get; set; }
    }
}
