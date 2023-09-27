using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using OAOPS.Shared.DTO;

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

        [HttpPost]
        public IActionResult AddStorage(StorageDto storage)
        {
            int error = StorageService.AddStorage(storage);
            var res = errorCodeService.GetError(error);
            if (!res.IsSuccessCode)
            {
                return BadRequest(new ErrorDto() { Code = res.Code, ErrorText = res.ErrorText, IsSuccessCode = res.IsSuccessCode});
            }
            else
            {
                return Ok(new ErrorDto() { Code = res.Code, ErrorText = res.ErrorText, IsSuccessCode = res.IsSuccessCode });
            }
        }

        [HttpPost]
        public IActionResult AddStorageSlot(StorageSlotDto storageSlot)
        {
            int error = StorageService.AddStorageSlot(storageSlot);
            var res = errorCodeService.GetError(error);
            if (!res.IsSuccessCode)
            {
                return BadRequest(new ErrorDto() { Code = res.Code, ErrorText = res.ErrorText, IsSuccessCode = res.IsSuccessCode });
            }
            else
            {
                return Ok(new ErrorDto() { Code = res.Code, ErrorText = res.ErrorText, IsSuccessCode = res.IsSuccessCode });
            }
        }
    }
}
