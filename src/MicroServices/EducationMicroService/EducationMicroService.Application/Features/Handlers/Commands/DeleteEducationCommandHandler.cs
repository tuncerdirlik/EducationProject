using EducationMicroService.Application.Contracts;
using EducationMicroService.Application.DTOs;
using EducationMicroService.Application.Features.Requests.Commands;
using MediatR;

namespace EducationMicroService.Application.Features.Handlers.Commands
{
    public class DeleteEducationCommandHandler : IRequestHandler<DeleteEducationCommand, BaseResponse<EducationDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteEducationCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<EducationDto>> Handle(DeleteEducationCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<EducationDto>();

            var education = await _unitOfWork.EducationRepository.GetAsync(request.Id);
            if (education == null)
            {
                response.Success = false;
                response.Errors = new List<string> { "Item not found" };

                return response;
            }

            await _unitOfWork.EducationRepository.DeleteAsync(education);
            await _unitOfWork.SaveAsync();

            response.Success = true;
            return response;
        }
    }
}
