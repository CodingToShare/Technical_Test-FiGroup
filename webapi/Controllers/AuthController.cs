using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using webapi.Exceptions;
using webapi.Interfaces;
using webapi.ModelDto;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private IAuthService _authService;

        public AuthController(UserManager<IdentityUser> userManager, IAuthService authService)
        {
            _userManager = userManager;
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            try
            {
                var result = await _authService.Register(model);

                return Ok(result);
            }
            catch (AccountRegisterException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            try
            {
                var result = await _authService.Login(model);

                return Ok(result);
            }
            catch (LoginCredentialsException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
