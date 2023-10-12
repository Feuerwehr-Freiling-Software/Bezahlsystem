using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OAOPS.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ArticleCategoryDto>>> GetCategories()
        {
            return await categoryService.GetCategories();
        }

        [HttpPost]
        public async Task<ActionResult<ErrorDto>> AddCategory(ArticleCategoryDto category)
        {
            return await categoryService.AddCategory(category);
        }

        [HttpPut]
        public async Task<ActionResult<ErrorDto>> UpdateCategory(ArticleCategoryDto category)
        {
            return await categoryService.UpdateCategory(category);
        }

        [HttpDelete("{categoryId:int}")]
        public async Task<ActionResult<ErrorDto>> DeleteCategory(int categoryId)
        {
            return await categoryService.DeleteCategory(categoryId);
        }

        [HttpGet]
        public async Task<ActionResult<List<ArticleCategoryDto>>> GetAllCategories()
        {
            return await categoryService.GetAllCategories();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategoriesShort()
        {
            return Ok(await categoryService.GetAllCategoriesShort());
        }
    }
}
