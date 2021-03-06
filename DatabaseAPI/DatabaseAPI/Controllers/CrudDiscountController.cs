﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseAPI.ApiModels;
using DatabaseAPI.Services;
namespace DatabaseAPI.Controllers
{
    [Route("/api/crud/Discount")]
    [ApiController]
    public class CrudDiscountController : ControllerBase
    {
        ICrudDiscountServices _services;

        public CrudDiscountController(ICrudDiscountServices services)
        {
            _services = services;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> PostDiscount([FromHeader] string sessionId, [FromBody] ApiModels.CRUD.Discount discount)
        {
            if (Startup.ActiveSessions.ContainsKey(sessionId) && Startup.ActiveSessions[sessionId].Type == DatabaseModels.UserType.Admin)
            {
                var result = await _services.Create(new DatabaseModels.Discount(discount.Id, discount.IsAvailable, discount.SetPrice, discount.PriceDropAmmount, discount.PriceDropPercent));
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
        public async Task<IActionResult> GetDiscountById([FromRoute] Guid guid, [FromHeader] string sessionId)
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
        public async Task<IActionResult> UpdateDiscount([FromHeader] string sessionId, [FromBody] ApiModels.CRUD.Discount discount)
        {
            if (Startup.ActiveSessions.ContainsKey(sessionId) && Startup.ActiveSessions[sessionId].Type == DatabaseModels.UserType.Admin)
            {
                var result = await _services.Update(new DatabaseModels.Discount(discount.Id, discount.IsAvailable, discount.SetPrice, discount.PriceDropAmmount, discount.PriceDropPercent));
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
        public async Task<IActionResult> DeleteDiscount([FromHeader] string sessionId, [FromBody] ApiModels.CRUD.Discount discount)
        {
            if (Startup.ActiveSessions.ContainsKey(sessionId) && Startup.ActiveSessions[sessionId].Type == DatabaseModels.UserType.Admin)
            {
                var result = await _services.Delete(new DatabaseModels.Discount(discount.Id, discount.IsAvailable, discount.SetPrice, discount.PriceDropAmmount, discount.PriceDropPercent));
                return Ok(result);
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
