using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SampleProject.Application.Features.Companies.Commands.Companies.Request;
using SampleProject.Application.Features.Companies.Queries.Companies.Request;
using SampleProject.Application.Mvc.Filters;
using SampleProject.Shared.Mvc.Filters;

namespace SampleProject.Api.Controllers
{
    [Route("api/companies")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [Authorize(Policy = AppRolePolicy.USER_ACCESS)]
    public class CompanyController(ISender sender) : ControllerBase
    {
        /// <summary>
        ///   Get a page of Company.
        /// </summary>
        /// <param name="request">The request for get list of Company.</param>
        /// <param name="cancellationToken">The cancellation token to abort execution.</param>
        /// <returns>Returns the page.</returns>
        [HttpGet]
        public async Task<dynamic> GetAsync([FromQuery] CompanyListRequest request, CancellationToken cancellationToken)
        {
            var result = await sender.Send(request, cancellationToken);
            return new BaseActionResult(result);
        }

        /// <summary>
        ///   Get a Company.
        /// </summary>
        /// <param name="id">The ad id of the Company.</param>
        /// <param name="cancellationToken">The cancellation token to abort execution.</param>
        /// <returns>Returns the Company.</returns>
        [HttpGet("{id:guid}")]
        public async Task<dynamic> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            var request = new CompanyGetByIdRequest { Id = id };
            var result = await sender.Send(request, cancellationToken);
            return new BaseActionResult(result);
        }

        /// <summary>
        ///   Create a new Company.
        /// </summary>
        /// <param name="request">The request body when create a new Company.</param>
        /// <param name="cancellationToken">The cancellation token to abort execution.</param>
        /// <returns>Returns.</returns>
        [HttpPost]
        public async Task<dynamic> PostAsync([FromBody] CompanyCreateRequest request, CancellationToken cancellationToken)
        {
            var result = await sender.Send(request, cancellationToken);
            return new BaseActionResult(result);
        }

        /// <summary>
        ///   Update a Company.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request">The request body when update a Company.</param>
        /// <param name="cancellationToken">The cancellation token to abort execution.</param>
        /// <returns>Returns.</returns>
        [HttpPut("{id:guid}")]
        public async Task<dynamic> PutAsync(Guid id, [FromBody] CompanyEditRequest request, CancellationToken cancellationToken)
        {
            request.Id = id;
            var result = await sender.Send(request, cancellationToken);
            return new BaseActionResult(result);
        }

        /// <summary>
        ///   Delete a Company.
        /// </summary>
        /// <param name="id">The id of the Company.</param>
        /// <param name="cancellationToken">The cancellation token to abort execution.</param>
        /// <returns>Returns.</returns>
        [HttpDelete("{id:guid}")]
        public async Task<dynamic> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var request = new CompanyDeleteRequest
            {
                Id = id
            };
            var result = await sender.Send(request, cancellationToken);
            return new BaseActionResult(result);
        }
    }
}
