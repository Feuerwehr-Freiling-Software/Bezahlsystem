﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace OAOPS.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        public IUserService userService { get; }

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }


        [HttpGet, AllowAnonymous]
        public async Task<ActionResult<double>> GetBalance(string username)
        {
            var balance = await userService.GetUserBalance(username);
            return balance;
        }

        [HttpGet, Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsers()
        {
            List<UserDto> users = await userService.GetAllUsers();
            return Ok(users);
        }

        [HttpGet, Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUsersFiltered(string? username = null, int? page = null, int? pageSize = null)
        {
            List<UserDto> users = await userService.GetUsersFiltered(username, page, pageSize);
            return Ok(users);
        }

        [HttpGet, Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUserStats(string username)
        {
            if (username == null) return BadRequest();
            UserStatsDto stats = await userService.GetUserStats(username);
            return Ok(stats);
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> GetAllPaymentsFiltered(DateTime? fromDate = null, DateTime? toDate = null, string? category = null, double? minAmount = null, double? maxAmount = null)
        {
            var res = await userService.GetAllPaymentsFiltered(fromDate, toDate, category, minAmount, maxAmount);
            return Ok(res);
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> GetAllTopupsFiltered(string username, DateTime? fromDate = null, DateTime? toDate = null, string? executor = null, double? amount = null)
        {
            List<TopUpDto> res = await userService.GetAllTopupsFiltered(username, fromDate, toDate, executor, amount);
            return Ok(res);
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> GetRoles()
        {
            List<RoleDto> roles = await userService.GetRoles();
            return Ok(roles);
        }

        [HttpPut, Authorize]
        public async Task<IActionResult> UpdateUser(UserDto user)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "";
            ErrorDto? res = await userService.UpdateUser(user, userId);
            if (res == null) return BadRequest();
            return Ok(res);
        }

        [HttpPost, Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddTopUp(AddTopupDto topUp)
        {
            ErrorDto? res = await userService.AddTopUp(topUp);
            if (res == null) return BadRequest();
            return Ok(res);
        }
    }
}
