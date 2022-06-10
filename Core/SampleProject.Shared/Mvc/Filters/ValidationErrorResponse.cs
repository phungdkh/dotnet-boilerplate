namespace SampleProject.Shared.Mvc.Filters
{
    /// <summary>
    /// The json validation error response object.
    /// </summary>
    public class ValidationErrorResponse
    {
        /// <summary>
        /// Gets or Sets the json error message.
        /// </summary>
        public ICollection<InvalidModel> Messages { get; set; } = [];
    }

    public record InvalidModel(string PropertyName, string ErrorMessage);
}
