using AutoMapper;
using EducationMicroService.Application.Contracts;
using EducationMicroService.Application.DTOs;
using EducationMicroService.Application.Features.Requests.Commands;
using MediatR;

namespace EducationMicroService.Application.Features.Handlers.Commands
{
    public class UpdateEducationCommandHandler : IRequestHandler<UpdateEducationCommand, BaseResponse<EducationDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public UpdateEducationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<EducationDto>> Handle(UpdateEducationCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<EducationDto>();
            var educationdbItem = await _unitOfWork.EducationRepository.GetAsync(request.EducationDto.Id);
            if (educationdbItem == null)
            {
                response.Success = false;
                response.Errors = new List<string> { "Item not found" };

                return response;
            }

            _mapper.Map(request.EducationDto, educationdbItem);
            await _unitOfWork.EducationRepository.UpdateAsync(educationdbItem);
            await _unitOfWork.SaveAsync();

            response.Success = true;
            response.Data = request.EducationDto;

            return response;
        }
    }
}
