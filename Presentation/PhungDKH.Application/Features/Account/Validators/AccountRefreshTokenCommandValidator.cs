using FluentValidation;
using PhungDKH.Application.Common.Constants;
using PhungDKH.Application.Features.Account.Commands;

namespace PhungDKH.Application.Features.Account.Validators
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
