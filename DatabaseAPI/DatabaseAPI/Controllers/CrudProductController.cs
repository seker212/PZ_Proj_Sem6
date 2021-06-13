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
    [Route("/api/crud/Protuct")]
    [ApiController]
    public class CrudProductController : ControllerBase
    {
        ICrudProductServices _services;

        public CrudProductController(ICrudProductServices services)
        {
            _services = services;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> PostProduct([FromHeader] string sessionId, [FromBody] DatabaseModels.Product product)
        {
            if (Startup.ActiveSessions.ContainsKey(sessionId) && Startup.ActiveSessions[sessionId].Type == DatabaseModels.UserType.Admin)
            {
                var result = await _services.Create(product);
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
        public async Task<IActionResult> GetProduct([FromHeader] string sessionId)
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
        public async Task<IActionResult> GetProductById([FromRoute] Guid guid, [FromHeader] string sessionId)
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
        public async Task<IActionResult> UpdateProduct([FromHeader] string sessionId, [FromBody] DatabaseModels.Product product)
        {
            if (Startup.ActiveSessions.ContainsKey(sessionId) && Startup.ActiveSessions[sessionId].Type == DatabaseModels.UserType.Admin)
            {
                var result = await _services.Update(product);
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
        public async Task<IActionResult> DeleteProduct([FromHeader] string sessionId, [FromBody] DatabaseModels.Product product)
        {
            if (Startup.ActiveSessions.ContainsKey(sessionId) && Startup.ActiveSessions[sessionId].Type == DatabaseModels.UserType.Admin)
            {
                var result = await _services.Delete(product);
                return Ok(result);
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
