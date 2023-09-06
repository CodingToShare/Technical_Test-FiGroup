using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using webapi.Data;
using webapi.Exceptions;
using webapi.Interfaces;
using webapi.ModelDto;

namespace webapi.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IJwtSettings _jwtSettings;

        public AuthService(UserManager<IdentityUser> userManager, IJwtSettings jwtSettings)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings;
        }

        public async Task<string> Login(LoginModel model)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Email == model.Email) ?? throw new LoginCredentialsException("Email or password incorrect");

            var userSigninResult = await _userManager.CheckPasswordAsync(user, model.Password);

            if (userSigninResult)
            {
                var roles = await _userManager.GetRolesAsync(user);

                return GenerateJwt(user, roles);
            }

            throw new LoginCredentialsException("Email or password incorrect");
        }

        public async Task<bool> Register(RegisterModel model)
        {
            var user = new IdentityUser()
            {
                Email = model.Email,
                UserName = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return true;
            }

            var errors = "";

            foreach (var error in result.Errors)
            {
                if (!errors.IsNullOrEmpty())
                {
                    errors += " | ";
                }

                errors += error.Description;
            }

            throw new AccountRegisterException(errors);
        }

        private string GenerateJwt(IdentityUser user, IList<string> roles)
        {
            if(user.UserName != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                };

                var roleClaims = roles.Select(r => new Claim(ClaimTypes.Role, r));
                claims.AddRange(roleClaims);

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var expires = DateTime.Now.AddDays(Convert.ToDouble(_jwtSettings.ExpirationInDays));

                var token = new JwtSecurityToken(
                    issuer: _jwtSettings.Issuer,
                    audience: _jwtSettings.Issuer,
                    claims,
                    expires: expires,
                    signingCredentials: creds
                );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            else
            {
                return "error token";
            }           
        }
    }
}
