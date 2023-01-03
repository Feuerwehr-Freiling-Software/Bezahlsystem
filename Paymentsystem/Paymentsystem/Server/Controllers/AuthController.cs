using Paymentsystem.Shared.Extensions;
using Paymentsystem.Shared.Interfaces;

namespace Paymentsystem.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public AuthController(IConfiguration config, IUserService userService, ITokenService tokenService)
        {
            _config = config;
            _userService = userService;
            _tokenService = tokenService;
        }

        // Role authentication https://www.youtube.com/watch?v=TDY_DtTEkes
        [HttpPost]
        public async Task<IActionResult> Register(UserDto req)
        {
            // TODO: Implement Db Check
            var user = new User()
            {
                Id = req.Username.ToSha256(),
                Balance = 0,
                Email = req.Email,
                Firstname = req.FirstName,
                Lastname = req.LastName,
                IsConfirmedUser = false,
                Role = "User",
                Username = req.Username,
                Comment = "",
                ConfirmedEmail = false,
                Refreshtoken = new()
                {
                    Created = DateTime.UtcNow,
                    Expires = DateTime.UtcNow,
                    Token = ""
                },
                EmailConfirmationCode = new()
                {
                    ConfirmationCode = GenerateConfirmationCode(),
                    Created = DateTime.Now,
                    Expires = DateTime.Now.AddHours(1)
                }
            };

            // TODO: Send Confirmation Email to user
            
            CreatePasswordHash(req.Password, out byte[] hash, out byte[] salt);

            user.PasswordHash = hash;
            user.PasswordSalt = salt;

            // Add User to db
            _userService.AddUser(user);

            return Ok(user);

        }

        private string GenerateConfirmationCode()
        {
            var random = new Random();
            return random.Next(0, 1000000).ToString("D6");
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmEmail(string code)
        {
            // TODO: Check for confirmation Code in Db and check if user is authenticated
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDto req)
        {
            // check if user exists
            var user = _userService.GetByUsername(req.Username);
            if (user == null) return BadRequest("Benutzername oder Passwort ist falsch");
            if (!VerifyPasswordHash(req.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Benutzername oder Passwort ist falsch.");
            }

            var refreshToken = GenerateRefreshToken();
            SetRefreshToken(refreshToken, user.Username);

            string token = CreateToken(user);
            return Ok(token);
        }

        [HttpPost]
        public IActionResult RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var user = _userService.GetByUsername(User.Identity.Name);
            // TODO: Change to DB
            if (!user.Refreshtoken.Equals(refreshToken))
            {
                return Unauthorized("Invalid Refresh Token");
            }
            else if (user.Refreshtoken.Expires < DateTime.Now)
            {
                return Unauthorized("Token expired.");
            }

            string token = CreateToken(user);
            var newRefreshToken = GenerateRefreshToken();
            SetRefreshToken(newRefreshToken, user.Username);

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

        private void SetRefreshToken(RefreshToken token, string username)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = token.Expires
            };

            // TODO: Move to Db Service
            Response.Cookies.Append("refreshToken", token.Token, cookieOptions);
            _tokenService.UpdateTokenFromUserByName(username, token.Token, token.Created, token.Expires);
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:TokenKey").Value));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(1),
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
