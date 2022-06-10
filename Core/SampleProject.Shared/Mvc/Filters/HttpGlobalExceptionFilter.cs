using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using SampleProject.Shared.Common.Constants;
using SampleProject.Shared.Mvc.ActionResults;

namespace SampleProject.Shared.Mvc.Filters
{
    /// <inheritdoc />
    /// <summary>
    ///   The handle exception.
    /// </summary>
    /// <remarks>
    ///   Initializes a new instance of the <see cref="HttpGlobalExceptionFilter" /> class.
    /// </remarks>
    /// <param name="logger">The logger.</param>
    public class HttpGlobalExceptionFilter(ILogger<HttpGlobalExceptionFilter> logger) : IExceptionFilter
    {
        private readonly ILogger<HttpGlobalExceptionFilter> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        /// <inheritdoc />
        public void OnException(ExceptionContext context)
        {
            ArgumentNullException.ThrowIfNull(context);

            _logger.LogTrace(
                new EventId(context.Exception.HResult),
                context.Exception,
                context.Exception.Message);

            switch (context.Exception)
            {
                case ValidationException validationException:
                    var jsonValidation = new ValidationErrorResponse
                    {
                        Messages = validationException.Errors
                            .Select(e => new InvalidModel(e.PropertyName, e.ErrorMessage))
                            .ToList()
                    };

                    context.Result = new BadRequestObjectResult(jsonValidation);
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                default:
                    var jsonException = new JsonErrorResponse
                    {
                        Messages = [AppMessageConstants.ERROR_OCCURRED],
                        DeveloperMessage = context.Exception
                    };

                    context.Result = new InternalServerErrorObjectResult(jsonException);
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
            }

            context.ExceptionHandled = true;
        }
    }
}
