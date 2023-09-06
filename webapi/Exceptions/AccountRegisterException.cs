namespace webapi.Exceptions
{
    public class AccountRegisterException : Exception
    {
        public AccountRegisterException()
        {
        }

        public AccountRegisterException(string? message) : base(message)
        {
        }

        public AccountRegisterException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
