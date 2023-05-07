using AutoMapper;
using Core.CacheService;
using EducationMicroService.Application.Contracts;
using EducationMicroService.Application.DTOs;
using EducationMicroService.Application.Features.Requests.Queries;
using EducationMicroService.Domain;
using MediatR;

namespace EducationMicroService.Application.Features.Handlers.Queries
{
    public class GetEducationItemRequestHandler : IRequestHandler<GetEducationItemRequest, BaseResponse<EducationDto>>
    {
        private IMapper _mapper;
        private IEducationRepository _educationRepository;
        private IDistributedCacheService<Education> _distributedCacheService;

        public GetEducationItemRequestHandler(IMapper mapper, IEducationRepository educationRepository, IDistributedCacheService<Education> distributedCacheService)
        {
            _mapper = mapper;
            _educationRepository = educationRepository;
            _distributedCacheService = distributedCacheService;
        }

        public async Task<BaseResponse<EducationDto>> Handle(GetEducationItemRequest request, CancellationToken cancellationToken)
        {
            Education? education = await GetFromCache(request.Id);
            if (education is null)
            {
                education = await _educationRepository.GetAsync(request.Id);
            }
            
            var respone = new BaseResponse<EducationDto>();
            respone.Success = true;
            respone.Data = _mapper.Map<EducationDto>(education);

            return respone;
        }

        private async Task<Education?> GetFromCache(int id)
        {
            return await _distributedCacheService.GetById(id);
        }
    }
}
