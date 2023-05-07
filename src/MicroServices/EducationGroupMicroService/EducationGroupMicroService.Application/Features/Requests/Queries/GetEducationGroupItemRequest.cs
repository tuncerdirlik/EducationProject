using EducationGroupMicroService.Application.DTOs;
using MediatR;

namespace EducationGroupMicroService.Application.Features.Requests.Queries
{
    public class GetEducationGroupItemRequest : IRequest<BaseResponse<EducationGroupDto>>
    {
        public int Id { get; set; }
    }
}
