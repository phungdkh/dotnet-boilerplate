using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SampleProject.Shared.Mvc.Filters;
using SampleProject.Application.Features.Me.Queries.Requests;
using SampleProject.Application.Mvc.Filters;

namespace SampleProject.Api.Controllers
{
    /// <summary>
    ///   Initializes a new instance of the <see cref="MeController" /> class.
    /// </summary>
    /// <param name="mediator">The mediator.</param>
    [Route("api/me")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [Authorize(Policy = AppRolePolicy.USER_ACCESS)]
    public class MeController(ISender mediator) : ControllerBase
    {
        [HttpGet("profile")]
        public async Task<dynamic> Profile(CancellationToken cancellationToken)
        {
            var request = new ProfileRequest()
            {
            };

            var result = await mediator.Send(request, cancellationToken);
            return new BaseActionResult(result);
        }
    }
}
