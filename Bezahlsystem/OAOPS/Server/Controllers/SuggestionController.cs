using Duende.IdentityServer.Extensions;
using System.Security.Claims;

namespace OAOPS.Server.Controllers
{
    [Route("api/[controller]/[action]/{id?}")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class SuggestionController : ControllerBase
    {
        private readonly ISuggestionService _suggestionService;
        private readonly ILoggerService loggerService;
        private readonly UserManager<ApplicationUser> _userManager;

        public SuggestionController(ISuggestionService suggestionService, ILoggerService _loggerService, UserManager<ApplicationUser> userManager)
        {
            _suggestionService = suggestionService;
            loggerService = _loggerService;
            _userManager = userManager;
        }

        [HttpPost, AllowAnonymous]
        public async Task<ActionResult<ErrorCode>> AddSuggestion(SuggestionDTO suggestion)
        {
            var tmp = User.Identity.Name;

            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var username = _userManager.FindByIdAsync(userId).Result.UserName;

            if (username == null) return BadRequest();

            var res = _suggestionService.AddSuggestion(suggestion, username);

            return Ok();
        }

        [HttpGet]
        public ActionResult<List<SuggestionDTO>> GetAllSuggestions()
        {
            var list = _suggestionService.GetSuggestions();
            return Ok(list);
        }
    }
}
