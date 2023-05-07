using AutoMapper;
using EducationGroupMicroService.Application.Contracts;
using EducationGroupMicroService.Application.DTOs;
using EducationGroupMicroService.Application.Features.Requests.Commands;
using MediatR;

namespace EducationGroupMicroService.Application.Features.Handlers.Commands
{
    public class UpdateEducationGroupCommandHandler : IRequestHandler<UpdateEducationGroupCommand, BaseResponse<EducationGroupDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public UpdateEducationGroupCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<EducationGroupDto>> Handle(UpdateEducationGroupCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<EducationGroupDto>();
            var educationGroup = await _unitOfWork.EducationGroupRepository.GetAsync(request.EducationGroupDto.Id);
            if (educationGroup is null)
            {
                response.Success = false;
                response.Errors = new List<string> { "EducationGroup not found" };

                return response;
            }

            _mapper.Map(request.EducationGroupDto, educationGroup);
            await _unitOfWork.EducationGroupRepository.UpdateAsync(educationGroup);
            await _unitOfWork.SaveAsync();

            response.Success = true;
            response.Data = request.EducationGroupDto;

            return response;
        }
    }
}
