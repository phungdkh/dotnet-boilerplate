namespace SampleProject.Shared.Exceptions
{
    public class UserNotFoundException() : Exception("User can't be found.")
    {
        public static UserNotFoundException Instance { get; } = new();
    }
}
