using AutoMapper;
using Core.CacheService;
using EducationMicroService.Application.Contracts;
using EducationMicroService.Application.DTOs;
using EducationMicroService.Application.Features.Requests.Queries;
using EducationMicroService.Domain;
using MediatR;

namespace EducationMicroService.Application.Features.Handlers.Queries
{
    public class GetEducationListRequestHandler : IRequestHandler<GetEducationListRequest, BaseResponse<List<EducationDto>>>
    {
        private IMapper _mapper;
        private IEducationRepository _educationRepository;
        private IDistributedCacheService<Education> _distributedCacheService;

        public GetEducationListRequestHandler(IMapper mapper, IEducationRepository educationRepository, IDistributedCacheService<Education> distributedCacheService)
        {
            _mapper = mapper;
            _educationRepository = educationRepository;
            _distributedCacheService = distributedCacheService;
        }

        public async Task<BaseResponse<List<EducationDto>>> Handle(GetEducationListRequest request, CancellationToken cancellationToken)
        {
            IReadOnlyList<Education> educationList = await GetFromCache();
            if (educationList is null || !educationList.Any())
            {
                educationList = await _educationRepository.GetAllAsync();
            }

            var respone = new BaseResponse<List<EducationDto>>();
            respone.Success = true;
            respone.Data = _mapper.Map<List<EducationDto>>(educationList);

            return respone;
        }

        private async Task<List<Education>> GetFromCache()
        {
            return await _distributedCacheService.GetAll();
        }
    }
}
