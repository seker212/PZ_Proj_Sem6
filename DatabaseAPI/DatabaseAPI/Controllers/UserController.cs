using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseAPI.ApiModels;
using DatabaseAPI.Services;


namespace DatabaseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserServices _userServices;

        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        [HttpGet("login/cashier")]
        public async Task<IActionResult> LoginCashier([FromQuery] string username, [FromQuery] string password, [FromQuery] string cashierName)
        {
            var result = await _userServices.GetCashier(username, password, cashierName);
            if (result is null)
                return BadRequest();
            else
                return Ok(result);
        }

        [HttpGet("login/manager")]
        public async Task<IActionResult> LoginManager([FromQuery] string username, [FromQuery] string password)
        {
            var result = await _userServices.LogIn(username, password, DatabaseModels.UserType.Manager);
            if (result is null)
                return NotFound();
            else
                return Ok(result);
        }

        [HttpGet("login/admin")]
        public async Task<IActionResult> LoginAdmin([FromQuery] string username, [FromQuery] string password)
        {
            var result = await _userServices.LogIn(username, password, DatabaseModels.UserType.Manager);
            if (result is null)
                return NotFound();
            else
                return Ok(result);
        }

        [HttpDelete("logout")]
        public async Task<IActionResult> Logout([FromBody] string sessionId)
        {
            var result = await _userServices.LogOut(sessionId);
            if (result)
                return Ok();
            else
                return StatusCode(500);
        }
    }
}
