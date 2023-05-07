using AutoMapper;
using Core.CacheService;
using EducationGroupMicroService.Application.Contracts;
using EducationGroupMicroService.Application.DTOs;
using EducationGroupMicroService.Application.Features.Requests.Queries;
using EducationGroupMicroService.Domain;
using MediatR;

namespace EducationGroupMicroService.Application.Features.Handlers.Queries
{
    public class GetEducationGroupItemRequestHandler : IRequestHandler<GetEducationGroupItemRequest, BaseResponse<EducationGroupDto>>
    {
        private IMapper _mapper;
        private IEducationGroupRepository _educationGroupRepository;
        private IDistributedCacheService<EducationGroup> _distributedCacheService;

        public GetEducationGroupItemRequestHandler(IMapper mapper, IEducationGroupRepository educationGroupRepository, IDistributedCacheService<EducationGroup> distributedCacheService)
        {
            _mapper = mapper;
            _educationGroupRepository = educationGroupRepository;
            _distributedCacheService = distributedCacheService;
        }

        public async Task<BaseResponse<EducationGroupDto>> Handle(GetEducationGroupItemRequest request, CancellationToken cancellationToken)
        {
            EducationGroup? educationGroup = await GetFromCache(request.Id);
            if(educationGroup is null)
            {
                educationGroup = await _educationGroupRepository.GetAsync(request.Id);
            }

            var respone = new BaseResponse<EducationGroupDto>();
            respone.Success = true;
            respone.Data = _mapper.Map<EducationGroupDto>(educationGroup);

            return respone;
        }

        private async Task<EducationGroup?> GetFromCache(int id)
        {
            return await _distributedCacheService.GetById(id);
        }
    }
}
