using EducationMicroService.Application.DTOs;
using MediatR;

namespace EducationMicroService.Application.Features.Requests.Queries
{
    public class GetEducationListRequest : IRequest<BaseResponse<List<EducationDto>>>
    {
    }
}
