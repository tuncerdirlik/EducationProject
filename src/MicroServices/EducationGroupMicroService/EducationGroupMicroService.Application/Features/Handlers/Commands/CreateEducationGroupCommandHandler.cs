using AutoMapper;
using EducationGroupMicroService.Application.Contracts;
using EducationGroupMicroService.Application.DTOs;
using EducationGroupMicroService.Application.Features.Requests.Commands;
using EducationGroupMicroService.Domain;
using MediatR;

namespace EducationGroupMicroService.Application.Features.Handlers.Commands
{
    public class CreateEducationGroupCommandHandler : IRequestHandler<CreateEducationGroupCommand, BaseResponse<CreateEducationGroupDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public CreateEducationGroupCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<CreateEducationGroupDto>> Handle(CreateEducationGroupCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<CreateEducationGroupDto>();
            
            var educationGroup = _mapper.Map<EducationGroup>(request.CreateEducationGroupDto);
            var educationGroupInDb = await _unitOfWork.EducationGroupRepository.AddAsync(educationGroup);
            await _unitOfWork.SaveAsync();

            response.Success = true;
            response.Data = _mapper.Map<CreateEducationGroupDto>(educationGroupInDb);
            return response;
        }

    }
}
