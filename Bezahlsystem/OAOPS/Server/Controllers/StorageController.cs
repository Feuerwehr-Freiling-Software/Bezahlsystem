using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace OAOPS.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class StorageController : ControllerBase
    {
        public IStorageService StorageService { get; }

        public StorageController(IStorageService storageService)
        {
            StorageService = storageService;
        }

        [HttpGet, AllowAnonymous]
        public IActionResult GetAllStorages()
        {
            var req = Request;
            var user = User.Identity;

            var res = StorageService.GetAllStorages();
            return Ok(res);
        }
    }
}
