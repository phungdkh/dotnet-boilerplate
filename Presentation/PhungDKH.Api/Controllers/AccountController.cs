﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using PhungDKH.Application.Features.Account.Commands;
using PhungDKH.Shared.Mvc.Filters;

namespace PhungDKH.Api.Controllers
{
    /// <summary>
    ///   Initializes a new instance of the <see cref="sender" /> class.
    /// </summary>
    /// <param name="sender">The sender of mediator.</param>
    [Route("api/account")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class AccountController(ISender sender) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<dynamic> Register([FromBody] AccountRegisterCommand command, CancellationToken cancellationToken)
        {
            var result = await sender.Send(command, cancellationToken);
            return new BaseActionResult(result);
        }

        [HttpPost("login")]
        public async Task<dynamic> Login([FromBody] AccountLoginCommand command, CancellationToken cancellationToken)
        {
            var result = await sender.Send(command, cancellationToken);
            return new BaseActionResult(result);
        }

        [HttpPost("refresh")]
        public async Task<dynamic> Refresh([FromBody] AccountRefreshTokenCommand command, CancellationToken cancellationToken)
        {
            var result = await sender.Send(command, cancellationToken);
            return new BaseActionResult(result);
        }
    }
}
