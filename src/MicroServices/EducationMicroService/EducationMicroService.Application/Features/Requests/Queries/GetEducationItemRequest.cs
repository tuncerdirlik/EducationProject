using EducationMicroService.Application.DTOs;
using MediatR;

namespace EducationMicroService.Application.Features.Requests.Queries
{
    public class GetEducationItemRequest : IRequest<BaseResponse<EducationDto>>
    {
        public int Id { get; set; }
    }
}
