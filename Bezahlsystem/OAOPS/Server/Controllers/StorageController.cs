using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OAOPS.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize (Roles = "Admin")]
    public class StorageController : ControllerBase
    {
        public IStorageService StorageService { get; }

        public StorageController(IStorageService storageService)
        {
            StorageService = storageService;
        }

        [HttpGet]
        public IActionResult GetAllStorages()
        {
            var res = StorageService.GetAllStorages();
            return Ok(res);
        }
    }
}
