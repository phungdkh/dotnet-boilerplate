namespace PhungDKH.Shared.Exceptions
{
    public class InvalidRefreshTokenException() : Exception("Refresh token is not valid.")
    {
        public static InvalidRefreshTokenException Instance { get; } = new();
    }
}
