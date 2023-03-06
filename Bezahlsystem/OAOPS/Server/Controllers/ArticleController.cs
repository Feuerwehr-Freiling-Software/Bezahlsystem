using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OAOPS.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ArticleController : ControllerBase
    {
        public IArticleService _articleService { get; set; }

        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpGet, AllowAnonymous]
        public async Task<IActionResult> GetAllArticles()
        {
            return Ok(await _articleService.GetAllArticles());
        }
    }
}
