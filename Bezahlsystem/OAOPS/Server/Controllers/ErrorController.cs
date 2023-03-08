using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OAOPS.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ErrorController : ControllerBase
    {
        private readonly IErrorCodeService _errorSerice;

        public ErrorController(IErrorCodeService errorSerice)
        {
            _errorSerice = errorSerice;
        }

        [HttpGet]
        public IActionResult GetAllErrors()
        {
            var res = _errorSerice.GetAllErrors();
            return Ok(res);
        }

        [HttpPost]
        public IActionResult AddError(ErrorDto errorDto) 
        {
            ErrorCode errorCode = new()
            {
                Code = errorDto.Code,
                ErrorText = errorDto.ErrorText,
                IsSuccessCode = errorDto.IsSuccessCode
            };

            var res = _errorSerice.AddErrorCode(errorCode);
            var error = _errorSerice.GetError(res);

            var retList = new List<ErrorDto>() { new ErrorDto()
            {
                Code = error.Code,
                IsSuccessCode = error.IsSuccessCode,
                ErrorText = error.ErrorText
            } };

            if (error.IsSuccessCode)
            {
                return Ok(retList);
            }
            else
            {
                return BadRequest(retList);
            }

        }

    }
}
