using EducationMicroService.Application.DTOs;
using MediatR;

namespace EducationMicroService.Application.Features.Requests.Commands
{
    public class CreateEducationCommand : IRequest<BaseResponse<CreateEducationDto>>
    {
        public CreateEducationDto CreateEducationDto { get; set; }
    }
}
