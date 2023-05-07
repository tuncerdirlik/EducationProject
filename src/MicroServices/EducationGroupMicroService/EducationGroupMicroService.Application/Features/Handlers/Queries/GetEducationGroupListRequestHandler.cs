using AutoMapper;
using Core.CacheService;
using EducationGroupMicroService.Application.Contracts;
using EducationGroupMicroService.Application.DTOs;
using EducationGroupMicroService.Application.Features.Requests.Queries;
using EducationGroupMicroService.Domain;
using MediatR;

namespace EducationGroupMicroService.Application.Features.Handlers.Queries
{
    public class GetEducationGroupListRequestHandler : IRequestHandler<GetEducationGroupListRequest, BaseResponse<List<EducationGroupDto>>>
    {
        private IMapper _mapper;
        private IEducationGroupRepository _educationGroupRepository;
        private IDistributedCacheService<EducationGroup> _distributedCacheService;

        public GetEducationGroupListRequestHandler(IMapper mapper, IEducationGroupRepository educationGroupRepository, IDistributedCacheService<EducationGroup> distributedCacheService)
        {
            _mapper = mapper;
            _educationGroupRepository = educationGroupRepository;
            _distributedCacheService = distributedCacheService;
        }

        public async Task<BaseResponse<List<EducationGroupDto>>> Handle(GetEducationGroupListRequest request, CancellationToken cancellationToken)
        {
            IReadOnlyList<EducationGroup> educationGroupList = await GetFromCache();
            if (educationGroupList is null || !educationGroupList.Any())
            {
                educationGroupList = await _educationGroupRepository.GetAllAsync();
            }

            var respone = new BaseResponse<List<EducationGroupDto>>();
            respone.Success = true;
            respone.Data = _mapper.Map<List<EducationGroupDto>>(educationGroupList);

            return respone;
        }

        private async Task<List<EducationGroup>> GetFromCache()
        {
            return await _distributedCacheService.GetAll();
        }
    }
}
