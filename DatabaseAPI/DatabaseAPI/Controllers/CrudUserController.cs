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
    [Route("/api/crud/User")]
    [ApiController]
    public class CrudUserController : ControllerBase
    {
        IUserServices _services;

        public CrudUserController(IUserServices services)
        {
            _services = services;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> PostDiscount([FromHeader] string sessionId, [FromBody] ApiModels.CRUD.User user)
        {
            if (Startup.ActiveSessions.ContainsKey(sessionId) && Startup.ActiveSessions[sessionId].Type == DatabaseModels.UserType.Admin)
            {
                var result = await _services.PostUser(new DatabaseModels.User(user.Id, user.Username, user.PasswordHash, user.Type));
                return Ok(result);
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetDiscount([FromHeader] string sessionId)
        {
            if (Startup.ActiveSessions.ContainsKey(sessionId) && Startup.ActiveSessions[sessionId].Type == DatabaseModels.UserType.Admin)
            {
                var result = await _services.GetUsers();
                return Ok(result);
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpGet("{guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDiscountById([FromRoute] Guid guid, [FromHeader] string sessionId)
        {
            if (Startup.ActiveSessions.ContainsKey(sessionId) && Startup.ActiveSessions[sessionId].Type == DatabaseModels.UserType.Admin)
            {
                var result = await _services.GetUser(guid);
                if (result is null)
                    return NotFound();
                return Ok(result);
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdateDiscount([FromHeader] string sessionId, [FromBody] ApiModels.CRUD.User user)
        {
            if (Startup.ActiveSessions.ContainsKey(sessionId) && Startup.ActiveSessions[sessionId].Type == DatabaseModels.UserType.Admin)
            {
                var result = await _services.UpdateUser(new DatabaseModels.User(user.Id, user.Username, user.PasswordHash, user.Type));
                return Ok(result);
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteDiscount([FromHeader] string sessionId, [FromBody] Guid id)
        {
            if (Startup.ActiveSessions.ContainsKey(sessionId) && Startup.ActiveSessions[sessionId].Type == DatabaseModels.UserType.Admin)
            {
                var result = await _services.DeleteUser(id);
                return Ok(result);
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
