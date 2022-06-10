using System.Net;
using Microsoft.AspNetCore.Mvc;
using SampleProject.Shared.Common.Models;

namespace SampleProject.Shared.Mvc.Filters
{
    public class BaseActionResult(ResponseModel responseModel) : IActionResult
    {
        public async Task ExecuteResultAsync(ActionContext context)
        {
            var objectResult = responseModel.StatusCode switch
            {
                HttpStatusCode.OK => new ObjectResult(responseModel.Data ?? responseModel.Message)
                {
                    StatusCode = (int)responseModel.StatusCode
                },
                _ => new ObjectResult(responseModel.Message) { StatusCode = (int)responseModel.StatusCode }
            };
            await objectResult.ExecuteResultAsync(context).ConfigureAwait(false);
        }
    }
}
