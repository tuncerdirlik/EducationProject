using AutoMapper;
using EducationMicroService.Application.Contracts;
using EducationMicroService.Application.DTOs;
using EducationMicroService.Application.Features.Requests.Commands;
using EducationMicroService.Domain;
using MediatR;

namespace EducationMicroService.Application.Features.Handlers.Commands
{
    public class CreateEducationCommandHandler : IRequestHandler<CreateEducationCommand, BaseResponse<CreateEducationDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public CreateEducationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<CreateEducationDto>> Handle(CreateEducationCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<CreateEducationDto>();

            var education = _mapper.Map<Education>(request.CreateEducationDto);
            var educationdbItem = await _unitOfWork.EducationRepository.AddAsync(education);
            await _unitOfWork.SaveAsync();

            response.Success = true;
            response.Data = _mapper.Map<CreateEducationDto>(educationdbItem);
            return response;
        }
    }
}
