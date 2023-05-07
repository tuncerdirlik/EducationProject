using FluentValidation;

namespace EducationGroupMicroService.Application.DTOs.Validators
{
    public class CreateEducationGroupDtoValidator : AbstractValidator<CreateEducationGroupDto>
    {
        public CreateEducationGroupDtoValidator()
        {
            RuleFor(k => k.Title)
                   .NotEmpty().WithMessage("{PropertyName} is required")
                   .MaximumLength(250).WithMessage("{PropertyName} must not be longer than 250");

            RuleFor(k => k.StartDate)
                    .GreaterThan(DateTime.MinValue).WithMessage("{PropertyName} must be a valid date")
                    .NotEmpty().WithMessage("{PropertyName} is required");

            RuleFor(k => k.EndDate)
                    .GreaterThan(DateTime.MinValue).WithMessage("{PropertyName} must be a valid date")
                    .NotEmpty().WithMessage("{PropertyName} is required");
        }


    }
}
