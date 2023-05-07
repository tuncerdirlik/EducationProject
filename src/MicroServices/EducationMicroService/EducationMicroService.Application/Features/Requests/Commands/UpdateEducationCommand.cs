using EducationMicroService.Application.DTOs;
using MediatR;

namespace EducationMicroService.Application.Features.Requests.Commands
{
    public class UpdateEducationCommand : IRequest<BaseResponse<EducationDto>>
    {
        public EducationDto EducationDto { get; set; }
    }
}
