using FluentValidation;
using SampleProject.Application.Common.Constants;
using SampleProject.Application.Features.Account.Commands;

namespace SampleProject.Application.Features.Account.Validators
{
    public sealed class AccountLoginCommandValidator : AbstractValidator<AccountLoginCommand>
    {
        public AccountLoginCommandValidator()
        {
            RuleFor(customer => customer.Email)
                .NotNull()
                .WithMessage(AppMessageConstants.EMAIL_IS_REQUIRED);
            RuleFor(customer => customer.Email)
                .EmailAddress()
                .WithMessage(AppMessageConstants.EMAIL_IS_INVALID);
            RuleFor(customer => customer.Password)
                .NotNull()
                .WithMessage(AppMessageConstants.PASSWORD_IS_REQUIRED);
        }
    }
}
