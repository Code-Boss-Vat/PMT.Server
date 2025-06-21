
using FluentValidation;
using PMT.Core.DTOs;

namespace PMT.Core.Validators;

public class OrganizationCreateRequestValidator : AbstractValidator<OrganizationCreateRequest>
{
    public OrganizationCreateRequestValidator()
    {
        RuleFor(req => req.Name)
            .NotEmpty().WithMessage("Organization name is required")
            .Length(5, 20).WithMessage($"Organization name should be between {0} and {1} in length.");

        RuleFor(req => req.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required");

        RuleFor(req => req.Email)
            .NotEmpty().WithMessage("Email is required");

        RuleFor(req => req.Email)
            .NotEmpty().WithMessage("Organization address is required");
    }
}
