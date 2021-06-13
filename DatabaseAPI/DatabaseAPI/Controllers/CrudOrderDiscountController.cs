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
    [Route("/api/crud/OrderDiscount")]
    [ApiController]
    public class CrudOrderDiscountController : ControllerBase
    {
        ICrudPairServices<DatabaseModels.OrderDiscount> _services;

        public CrudOrderDiscountController(ICrudPairServices<OrderDiscount> services)
        {
            _services = services;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Post([FromHeader] string sessionId, [FromBody] ApiModels.CRUD.OrderDiscount orderDiscount)
        {
            if (Startup.ActiveSessions.ContainsKey(sessionId) && Startup.ActiveSessions[sessionId].Type == DatabaseModels.UserType.Admin)
            {
                var result = await _services.Create(new OrderDiscount(orderDiscount.OrderId, orderDiscount.DiscountId, orderDiscount.Quantity));
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
        public async Task<IActionResult> GetById([FromQuery] Guid orderId, [FromQuery] Guid discountId, [FromHeader] string sessionId)
        {
            if (Startup.ActiveSessions.ContainsKey(sessionId))
            {
                var result = await _services.GetInstance(orderId, discountId);
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
        public async Task<IActionResult> Update([FromHeader] string sessionId, [FromBody] ApiModels.CRUD.OrderDiscount orderDiscount)
        {
            if (Startup.ActiveSessions.ContainsKey(sessionId) && Startup.ActiveSessions[sessionId].Type == DatabaseModels.UserType.Admin)
            {
                var result = await _services.Update(new OrderDiscount(orderDiscount.OrderId, orderDiscount.DiscountId, orderDiscount.Quantity));
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
        public async Task<IActionResult> Delete([FromHeader] string sessionId, [FromBody] ApiModels.CRUD.OrderDiscount orderDiscount)
        {
            if (Startup.ActiveSessions.ContainsKey(sessionId) && Startup.ActiveSessions[sessionId].Type == DatabaseModels.UserType.Admin)
            {
                var result = await _services.Delete(orderDiscount.OrderId, orderDiscount.DiscountId);
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
