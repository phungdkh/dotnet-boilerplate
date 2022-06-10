using FluentValidation;
using Microsoft.AspNetCore.Identity;
using SampleProject.Application.Common.Constants;
using SampleProject.Application.Features.Account.Commands;
using SampleProject.Domain.Entities;

namespace SampleProject.Application.Features.Account.Validators
{
    public sealed class AccountRegisterCommandValidator : AbstractValidator<AccountRegisterCommand>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountRegisterCommandValidator(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;

            RuleFor(customer => customer.Email)
                .NotNull()
                .WithMessage(AppMessageConstants.EMAIL_IS_REQUIRED);
            RuleFor(customer => customer.Email)
                .Must(uniqueMail)
                .WithMessage(AppMessageConstants.EMAIL_IS_ALREADY_TAKEN);
            RuleFor(customer => customer.Email)
                .EmailAddress()
                .WithMessage(AppMessageConstants.EMAIL_IS_INVALID);
            RuleFor(customer => customer.Phone)
                .NotNull()
                .WithMessage(AppMessageConstants.PHONE_IS_REQUIRED);
            RuleFor(customer => customer.Password)
                .NotNull()
                .WithMessage(AppMessageConstants.PASSWORD_IS_REQUIRED);
        }

        private bool uniqueMail(string email)
        {
            var existedUser = _userManager.FindByEmailAsync(email).Result;
            return existedUser is null;
        }
    }
}
