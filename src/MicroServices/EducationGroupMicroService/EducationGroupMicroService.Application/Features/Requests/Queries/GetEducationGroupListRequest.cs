using EducationGroupMicroService.Application.DTOs;
using MediatR;

namespace EducationGroupMicroService.Application.Features.Requests.Queries
{
    public class GetEducationGroupListRequest : IRequest<BaseResponse<List<EducationGroupDto>>>
    {
    }
}
