using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SampleProject.Shared.Common.Models;

namespace SampleProject.Shared.Mvc.Filters
{
    public class ValidateModelFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var validationErrors = context.ModelState.Select(entry =>
                {
                    var key = entry.Key;
                    var err = context.ModelState[key]?.Errors.Select(e => e.ErrorMessage).ToArray();

                    return new InvalidModel
                    {
                        PropertyName = key,
                        ErrorMessage = string.Join(";", err ?? throw new InvalidOperationException($"Model state exception"))
                    };
                });

                var json = new ResponseModel { Errors = validationErrors.ToArray(), StatusCode = HttpStatusCode.BadRequest };
                context.Result = new BadRequestObjectResult(json);
            }
            else
            {
                await next();
            }
        }

        public class InvalidModel
        {
            public required string PropertyName { get; set; }
            public required string ErrorMessage { get; set; }
        }
    }
}
