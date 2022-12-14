using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Paymentsystem.Server.Models;
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
            // TODO: Implement Db Check

            // TODO: Send Confirmation Email to user

            CreatePasswordHash(req.Password, out byte[] hash, out byte[] salt);

            user.Username = req.Username;
            user.PasswordHash = hash;
            user.PasswordSalt = salt;

            // Add User to db

            return Ok(user);

        }

        [HttpPost]
        public async Task<IActionResult> ConfirmEmail(string code)
        {
            // TODO: Check for confirmation Code in Db and check if user is authenticated
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserDto req)
        {
            // check if user exists
            if (user.Username != req.Username
                || !VerifyPasswordHash(req.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Benutzername oder Passwort ist falsch.");
            }

            var refreshToken = GenerateRefreshToken();
            SetRefreshToken(refreshToken);

            string token = CreateToken(user);
            return Ok(token);
        }

        [HttpPost]
        public IActionResult RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];

            // TODO: Change to DB
            if (!user.RefreshToken.Equals(refreshToken))
            {
                return Unauthorized("Invalid Refresh Token");
            }
            else if (user.TokenExpires < DateTime.Now)
            {
                return Unauthorized("Token expired.");
            }

            string token = CreateToken(user);
            var newRefreshToken = GenerateRefreshToken();
            SetRefreshToken(newRefreshToken);

            return Ok(token);
        }

        private RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                Created = DateTime.Now,
                Expires = DateTime.Now.AddMinutes(5),
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64))
            };

            return refreshToken;
        }

        private void SetRefreshToken(RefreshToken token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = token.Expires
            };

            // TODO: Move to Db Service
            Response.Cookies.Append("refreshToken", token.Token, cookieOptions);
            user.RefreshToken = token.Token;
            user.TokenCreated = token.Created;
            user.TokenExpires = token.Expires;
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:TokenKey").Value));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            
            return jwt;
        }

        [HttpGet]
        public IActionResult TestNoAuth()
        {
            return Ok("dei mamer");
        }

        [HttpGet, Authorize(Roles = "Admin")]
        public IActionResult TestAuth()
        {
            return Ok("ok, dei mamer");
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
