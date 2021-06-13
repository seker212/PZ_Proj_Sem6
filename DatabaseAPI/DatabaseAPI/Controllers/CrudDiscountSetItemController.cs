using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseAPI.ApiModels;
using DatabaseAPI.Services;
using DatabaseAPI.DatabaseModels;

namespace DatabaseAPI.Controllers
{
    [Route("/api/crud/DiscountSetItem")]
    [ApiController]
    public class CrudDiscountSetItemController : ControllerBase
    {
        ICrudPairServices<DatabaseModels.DiscountSetItem> _services;

        public CrudDiscountSetItemController(ICrudPairServices<DiscountSetItem> services)
        {
            _services = services;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Post([FromHeader] string sessionId, [FromBody] ApiModels.CRUD.DiscountSetItem discountSetItem)
        {
            if (Startup.ActiveSessions.ContainsKey(sessionId) && Startup.ActiveSessions[sessionId].Type == DatabaseModels.UserType.Admin)
            {
                var result = await _services.Create(new DatabaseModels.DiscountSetItem(discountSetItem.DiscountId, discountSetItem.ProductId, discountSetItem.Quantity));
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
        public async Task<IActionResult> Get([FromHeader] string sessionId)
        {
            if (Startup.ActiveSessions.ContainsKey(sessionId))
            {
                var result = await _services.Get();
                return Ok(result);
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpGet("instance")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromQuery] Guid discountId, [FromQuery] Guid productId, [FromHeader] string sessionId)
        {
            if (Startup.ActiveSessions.ContainsKey(sessionId))
            {
                var result = await _services.GetInstance(discountId, productId);
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
        public async Task<IActionResult> Update([FromHeader] string sessionId, [FromBody] ApiModels.CRUD.DiscountSetItem discountSetItem)
        {
            if (Startup.ActiveSessions.ContainsKey(sessionId) && Startup.ActiveSessions[sessionId].Type == DatabaseModels.UserType.Admin)
            {
                var result = await _services.Update(new DatabaseModels.DiscountSetItem(discountSetItem.DiscountId, discountSetItem.ProductId, discountSetItem.Quantity));
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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromHeader] string sessionId, [FromBody] ApiModels.CRUD.DiscountSetItem discountSetItem)
        {
            if (Startup.ActiveSessions.ContainsKey(sessionId) && Startup.ActiveSessions[sessionId].Type == DatabaseModels.UserType.Admin)
            {
                var result = await _services.Delete(discountSetItem.DiscountId, discountSetItem.ProductId);
                if (result)
                    return Ok();
                else
                    return NotFound();
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
