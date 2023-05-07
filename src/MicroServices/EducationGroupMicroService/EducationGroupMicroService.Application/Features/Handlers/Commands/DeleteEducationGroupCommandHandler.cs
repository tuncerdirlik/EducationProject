using EducationGroupMicroService.Application.Contracts;
using EducationGroupMicroService.Application.DTOs;
using EducationGroupMicroService.Application.Features.Requests.Commands;
using MediatR;

namespace EducationGroupMicroService.Application.Features.Handlers.Commands
{
    public class DeleteEducationGroupCommandHandler : IRequestHandler<DeleteEducationGroupCommand, BaseResponse<EducationGroupDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteEducationGroupCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<EducationGroupDto>> Handle(DeleteEducationGroupCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<EducationGroupDto>();
            
            var educationGroup = await _unitOfWork.EducationGroupRepository.GetAsync(request.Id);
            if (educationGroup == null)
            {
                response.Success = false;
                response.Errors = new List<string> { "Item not found" };

                return response;
            }

            await _unitOfWork.EducationGroupRepository.DeleteAsync(educationGroup);
            await _unitOfWork.SaveAsync();

            response.Success = true;
            return response;
        }
    }
}
