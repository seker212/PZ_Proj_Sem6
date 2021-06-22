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
    [Route("/api/crud/Cashier")]
    [ApiController]
    public class CrudCashierController : ControllerBase
    {
        ICrudCashierServices _services;

        public CrudCashierController(ICrudCashierServices services)
        {
            _services = services;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> PostCashier([FromHeader] string sessionId, [FromBody] ApiModels.CRUD.Cashier cashier)
        {
            if (Startup.ActiveSessions.ContainsKey(sessionId) && Startup.ActiveSessions[sessionId].Type == DatabaseModels.UserType.Admin)
            {
                var result = await _services.Create(new DatabaseModels.Cashier(cashier.Id, cashier.FullName, cashier.Bilans));
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
        public async Task<IActionResult> GetCashier([FromHeader] string sessionId)
        {
            if (Startup.ActiveSessions.ContainsKey(sessionId))
            {
                var result = await _services.Read();
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
        public async Task<IActionResult> GetCashierById([FromRoute] Guid guid, [FromHeader] string sessionId)
        {
            if (Startup.ActiveSessions.ContainsKey(sessionId))
            {
                var result = await _services.Read(guid);
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
        public async Task<IActionResult> UpdateCashier([FromHeader] string sessionId, [FromBody] ApiModels.CRUD.Cashier cashier)
        {
            if (Startup.ActiveSessions.ContainsKey(sessionId) && Startup.ActiveSessions[sessionId].Type == DatabaseModels.UserType.Admin)
            {
                var result = await _services.Update(new DatabaseModels.Cashier(cashier.Id, cashier.FullName, cashier.Bilans));
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
        public async Task<IActionResult> DeleteCashier([FromHeader] string sessionId, [FromBody] ApiModels.CRUD.Cashier cashier)
        {
            if (Startup.ActiveSessions.ContainsKey(sessionId) && Startup.ActiveSessions[sessionId].Type == DatabaseModels.UserType.Admin)
            {
                var result = await _services.Delete(new DatabaseModels.Cashier(cashier.Id, cashier.FullName, cashier.Bilans));
                return Ok(result);
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
