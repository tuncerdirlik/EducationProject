using FluentValidation;

namespace EducationMicroService.Application.DTOs.Validators
{
    public class CreateEducationDtoValidator : AbstractValidator<CreateEducationDto>
    {
        public CreateEducationDtoValidator()
        {
            RuleFor(k => k.EducationGroupId)
                .NotNull().WithMessage("{PropertyName} is required")
                .GreaterThan(0).WithMessage("{PropertyName} must be valid");

            RuleFor(k => k.Title)
                    .NotEmpty().WithMessage("{PropertyName} is required")
                    .MaximumLength(250).WithMessage("{PropertyName} must not be longer than 250");

            RuleFor(k => k.Description)
                    .NotEmpty().WithMessage("{PropertyName} is required");

            RuleFor(k => k.Link)
                    .NotEmpty().WithMessage("{PropertyName} is required")
                    .MaximumLength(2000).WithMessage("{PropertyName} must not be longer than 2000");
        }
    }
}
