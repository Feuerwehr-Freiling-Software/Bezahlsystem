using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace OAOPS.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize (Roles = "Admin")]
    public class StorageController : ControllerBase
    {
        private readonly IErrorCodeService errorCodeService;

        public IStorageService StorageService { get; }

        public StorageController(IStorageService storageService, IErrorCodeService errorCodeService)
        {
            StorageService = storageService;
            this.errorCodeService = errorCodeService;
        }

        [HttpGet]
        public IActionResult GetAllStorages()
        {
            var req = Request;
            var user = User.Identity;

            var res = StorageService.GetAllStorages();
            return Ok(res);
        }

        [HttpGet]
        public IActionResult GetSlotsOfStorageByName(string name)
        {
            var res = StorageService.GetSlotsOfStorageByName(name);
            return Ok(res);
        }

        [HttpPut]
        public IActionResult UpdateStorageSlot(StorageSlotDto storageSlot)
        {
            int error = StorageService.UpdateStorageSlot(storageSlot);
            var res = errorCodeService.GetError(error);
            if (!res.IsSuccessCode)
            {
                return BadRequest(res);
            }
            else
            {
                return Ok(res);
            }
        }
    }
}
