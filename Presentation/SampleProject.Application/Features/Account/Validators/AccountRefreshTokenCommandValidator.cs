using FluentValidation;
using SampleProject.Application.Common.Constants;
using SampleProject.Application.Features.Account.Commands;

namespace SampleProject.Application.Features.Account.Validators
{
    public sealed class AccountRefreshTokenCommandValidator : AbstractValidator<AccountRefreshTokenCommand>
    {
        public AccountRefreshTokenCommandValidator()
        {
            RuleFor(customer => customer.RefreshToken)
                .NotNull()
                .WithMessage(AppMessageConstants.ACCOUNT_REFRESH_TOKEN_IS_REQUIRED);
        }
    }
}
