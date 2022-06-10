using System.Net;

namespace SampleProject.Shared.Common.Models
{
    public class ResponseModel
    {
        public HttpStatusCode StatusCode { get; set; }

        public object? Data { get; set; }

        public object[]? Errors { get; set; }

        public string? Message { get; set; }
    }
}
