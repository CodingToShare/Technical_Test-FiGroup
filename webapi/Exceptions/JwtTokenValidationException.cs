namespace webapi.Exceptions
{
    public class JwtTokenValidationException : Exception
    {
        public JwtTokenValidationException()
        {
        }

        public JwtTokenValidationException(string? message) : base(message)
        {
        }

        public JwtTokenValidationException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
