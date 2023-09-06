using webapi.ModelDto;

namespace webapi.Interfaces
{
    public interface IAuthService
    {
        public Task<bool> Register(RegisterModel model);
        public Task<string> Login(LoginModel model);
    }
}
