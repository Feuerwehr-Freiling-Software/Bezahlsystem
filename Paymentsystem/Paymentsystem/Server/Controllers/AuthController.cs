using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Paymentsystem.Shared.Models;
using Paymentsystem.Shared.ViewModels;
using System.Data.SqlTypes;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Paymentsystem.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static User user = new User();
        private readonly IConfiguration _config;

        public AuthController(IConfiguration config)
        {
            _config = config;
        }
        // Role authentication https://www.youtube.com/watch?v=TDY_DtTEkes
        [HttpPost]
        public async Task<IActionResult> Register(UserDto req)
        {
            CreatePasswordHash(req.Password, out byte[] hash, out byte[] salt);

            user.Username = req.Username;
            user.PasswordHash = hash;
            user.PasswordSalt = salt;

            return Ok(user);

        }

        [HttpPost]
        public async Task<IActionResult> Login(UserDto req)
        {
            // check if user exists
            if (user.Username != req.Username) return BadRequest("Username or Password is wrong.");
            if (!VerifyPasswordHash(req.Password, user.PasswordHash, user.PasswordSalt)) return BadRequest("Username or Password wrong.");

            string oldToken = "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTUxMiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiVG9ueSBTdGFyayIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6Iklyb24gTWFuIiwiZXhwIjozMTY4NTQwMDAwfQ.IbVQa1lNYYOzwso69xYfsMOHnQfO3VLvVqV2SOXS7sTtyyZ8DEf5jmmwz2FGLJJvZnQKZuieHnmHkg7CGkDbvA";
            string token = CreateToken(user);
            return Ok(token);
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.Name, user.Username)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            
            return jwt;
        }

        private void CreatePasswordHash(string password, out byte[] hash, out byte[] salt)
        {
            using(var hmac = new HMACSHA512())
            {
                salt = hmac.Key;
                hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] hash, byte[] salt)
        {
            using (var hmac = new HMACSHA512(salt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes((string)password));
                return computedHash.SequenceEqual(hash);
            }
        }
    }
}
