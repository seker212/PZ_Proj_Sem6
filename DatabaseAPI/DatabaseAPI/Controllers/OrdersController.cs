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
    [Route("/api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private IOrderServices _orderServices;

        public OrdersController(IOrderServices orderServices)
        {
            _orderServices = orderServices;
        }

        [HttpGet("kitchen")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<OrderProducts>))]
        public async Task<IEnumerable<OrderProducts>> GetKitchenOrders()
        {
            return await _orderServices.GetKitchenOrders(); 
        }

        [HttpGet("service")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<OrderProducts>))]
        public async Task<IEnumerable<OrderProducts>> GetServiceOrders()
        {
            return await _orderServices.GetServiceOrders();
        }

        //TODO: POSTS
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(OrderPost))]
        public async Task<IActionResult> PostOrder([FromBody] OrderPost order)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _orderServices.PostOrder(order);
            return Ok(result);
        }

        //[HttpPost("/backupOrders")]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //public IActionResult PostBackupOrders([FromBody] IEnumerable<OrderPost>)
        //{
        //}

        [HttpPut("kitchen/{orderId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateOrderToServing(Guid orderId)
        {
            if (orderId == Guid.Empty)
                return BadRequest();
            var result = await _orderServices.UpdateStatus(orderId, DatabaseModels.OrderStatus.Serving);
            if (result)
                return Ok();
            else
                return StatusCode(500);
        }

        [HttpPut("service/{orderId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateOrderToFinished(Guid orderId)
        {
            if (orderId == Guid.Empty)
                return BadRequest();
            var result = await _orderServices.UpdateStatus(orderId, DatabaseModels.OrderStatus.Finished);
            if (result)
                return Ok();
            else
                return StatusCode(500);
        }

        [HttpPut("cancelOrder/{orderId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> UpdateOrderToCanceled(Guid orderId)
        {
            if (orderId == Guid.Empty)
                return BadRequest();
            var result = await _orderServices.UpdateStatus(orderId, DatabaseModels.OrderStatus.Canceled);
            if (result)
                return Ok();
            else
                return StatusCode(500);
        }
    }
}
