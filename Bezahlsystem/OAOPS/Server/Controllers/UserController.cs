using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OAOPS.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        public IUserService userService { get; }

        [HttpGet]
        public async Task<ActionResult<double>> GetBalance(string username)
        {
            var balance = await userService.GetUserBalance(username);
            return balance;
        }
    }
}
