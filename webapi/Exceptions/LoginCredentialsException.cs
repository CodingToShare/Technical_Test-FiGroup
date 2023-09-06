namespace webapi.Exceptions
{
    public class LoginCredentialsException : Exception
    {
        public LoginCredentialsException() { }

        public LoginCredentialsException(string message) : base(message) { }

        public LoginCredentialsException(string message, Exception inner) : base(message, inner) { }
    }
}
