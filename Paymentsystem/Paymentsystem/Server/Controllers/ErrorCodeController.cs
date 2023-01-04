using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Paymentsystem.Shared.Interfaces;

namespace Paymentsystem.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ErrorCodeController : ControllerBase
    {
        private readonly IErrorCodeService errorCodeService;
        private readonly Errorcode EmptyErrorcode;
        public ErrorCodeController(IErrorCodeService errorCodeService)
        {
            this.errorCodeService = errorCodeService;
            this.EmptyErrorcode = new Errorcode()
            {
                Code = -1,
                ErrorText = "Error couldn't be found",
                IsSuccessErrorCode = false
            };
        }

        [HttpGet]
        public async Task<IActionResult> GetAllErrorCodes()
        {
            return Ok(errorCodeService.GetAllErrors());
        }

        [HttpGet("/{code}")]
        public async Task<IActionResult> GetErrorByCode(int code)
        {
            return Ok(errorCodeService.GetErrorcode(code));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateError(Errorcode errorcode)
        {
            var res = errorCodeService.UpdateError(errorcode);

            var method = System.Reflection.MethodBase.GetCurrentMethod();
            var fullName = method.Name + "." + method.ReflectedType.Name;

            var error = errorCodeService.GetErrorcode(res, fullName) ?? EmptyErrorcode;

            if (!error.IsSuccessErrorCode) return BadRequest(error);
            return Ok(error);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteError(Errorcode errorcode)
        {
            var res = errorCodeService.DeleteError(errorcode.Code); 
            
            var method = System.Reflection.MethodBase.GetCurrentMethod();
            var fullName = method.Name + "." + method.ReflectedType.Name;

            var error = errorCodeService.GetErrorcode(res, fullName) ?? EmptyErrorcode;

            if (!error.IsSuccessErrorCode) return BadRequest(error);
            return Ok(error);
        }

        [HttpPost]
        public async Task<IActionResult> AddError(Errorcode errorcode)
        {
            var res = errorCodeService.AddError(errorcode);

            var method = System.Reflection.MethodBase.GetCurrentMethod();
            var fullName = method.Name + "." + method.ReflectedType.Name;

            var error = errorCodeService.GetErrorcode(res, fullName) ?? EmptyErrorcode;

            if (!error.IsSuccessErrorCode) return BadRequest(error);
            return Ok(error);
        }
    }
}
