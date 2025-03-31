using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Claims;
using System.Text;

namespace TaskListSystemMVC.Helper
{
    public class AccountHelper : IAccountHelper
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public AccountHelper(IHttpContextAccessor _httpContextAccessor)
        {
            httpContextAccessor = _httpContextAccessor;
        }

        public string GetName()
        {
            var name = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value;
            return name ?? "Unknown";
        }

        public string HashPassword(string inputPassword)
        {
            byte[] saltstr = Encoding.UTF8.GetBytes("TaskListSystemMVC");
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: inputPassword,
                salt: saltstr,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
        }

        public bool VerifyPassword(string inputPassword, string storedHash)
        {
            return HashPassword(inputPassword) == storedHash;
        }
    }
}
