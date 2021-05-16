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
    [Route("/api/Discount")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        [HttpGet("available")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Discount>))]
        public async Task<IEnumerable<Discount>> GetAvailableDiscounts()
        {
            return new List<Discount>();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Discount>))]
        public async Task<IActionResult> GetDiscounts() //TODO: Add paging
        {
            return StatusCode(501);
        }

        [HttpPut("{guid}/status")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateStatus([FromRoute] Guid guid, [FromQuery] bool available)
        {
            return StatusCode(501);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> PostDiscount([FromBody] Discount discount)
        {
            return StatusCode(501);
        }
    }
}
