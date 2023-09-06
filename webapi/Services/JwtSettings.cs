using webapi.Interfaces;

namespace webapi.Services
{
    public class JwtSettings : IJwtSettings
    {
        public required string Issuer { get; set; }

        public required string Secret { get; set; }

        public int ExpirationInDays { get; set; }
    }
}
