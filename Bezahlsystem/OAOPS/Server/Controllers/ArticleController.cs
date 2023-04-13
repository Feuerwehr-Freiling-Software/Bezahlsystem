using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OAOPS.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class ArticleController : ControllerBase
    {
        public IArticleService _articleService { get; set; }
        readonly IErrorCodeService _errorService;

        public ArticleController(IArticleService articleService, IErrorCodeService errorService)
        {
            _articleService = articleService;
            _errorService = errorService;
        }

        [HttpGet, AllowAnonymous]
        public async Task<IActionResult> GetAllArticles()
        {
            return Ok(await _articleService.GetAllArticles());
        }

        [HttpPost]
        public async Task<IActionResult> AddArticle(ArticleDto article)
        {
            var res = await _articleService.AddArticle(article);

            var errors = await _errorService.GetErrorsFromList(res);

            return Ok(errors);
        }

        [HttpGet]
        public async Task<IActionResult> GetArticlesFiltered(string? articleName = null, int? page = null, int? pageSize = null)
        {
            List<ArticleDto> res = await _articleService.GetAllArticlesFiltered(articleName, page, pageSize);
            return Ok(res);
        }
    }
}
