using Microsoft.AspNetCore.Mvc;
using ShopApi.ApiModels;
using ShopApi.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopApi.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orders;

        public OrdersController(IOrderRepository orderRepository)
        {
            _orders = orderRepository;
        }

        [HttpGet("")]
        [ProducesResponseType(typeof(List<OrderModel>), 200)]
        public async Task<IActionResult> GetAllOrders()
        {
            List<OrderModel> orders = await _orders.GetAllOrders();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(OrderModel), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetOrder(int id)
        {
            OrderModel order = await _orders.GetOrder(id);
            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [HttpPost]
        [ProducesResponseType(typeof(OrderModel), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateOrder([FromBody] OrderModel order)
        {
            OrderModel createdOrder = await _orders.CreateOrder(order);

            return CreatedAtAction(
                nameof(GetOrder), new { id = createdOrder.Id }, createdOrder);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] OrderModel order)
        {
            try
            {
                await _orders.UpdateOrder(id, order);
                return Ok();
            }
            catch (EntityNotFoundException<OrderModel>)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            await _orders.DeleteOrder(id);
            return NoContent();
        }

        [HttpGet("{id}/details")]
        [ProducesResponseType(typeof(List<OrderDetailModel>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetDetail(int id)
        {
            List<OrderDetailModel> details = await _orders.GetDetailForOrder(id);
            if (details  == null)
            {
                return NotFound();
            }

            return Ok(details);
        }

        [HttpPost("{id}/details")]
        [ProducesResponseType(typeof(OrderDetailModel), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> AddDetail(int id, [FromBody] OrderDetailModel detail)
        {
            try
            {
                OrderDetailModel createdDetail = await _orders.AddDetailToOrder(id, detail);

                return CreatedAtAction(
                    nameof(GetDetail), new { orderId = id, rowId = createdDetail.Id }, createdDetail);
            }
            catch (EntityNotFoundException<OrderModel>)
            {
                return NotFound();
            }
        }

        [HttpGet("{orderId}/details/{detailId}")]
        [ProducesResponseType(typeof(OrderDetailModel), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetDetail(int orderId, int detailId)
        {
            OrderDetailModel row = await _orders.GetDetailInOrder(orderId, detailId);
            if (row == null)
            {
                return NotFound();
            }

            return Ok(row);
        }

        [HttpPut("{orderId}/details/{detailId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateRow(int orderId, int detailId, [FromBody] OrderDetailModel detail)
        {
            try
            {
                await _orders.UpdateDetailInOrder(orderId, detailId, detail);
                return NoContent();
            }
            catch (EntityNotFoundException<OrderDetailModel>)
            {
                return NotFound();
            }
            catch (EntityNotFoundException<OrderModel>)
            {
                return NotFound();
            }
        }

        [HttpDelete("{orderId}/details/{detailId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteRow(int orderId, int detailId)
        {
            try
            {
                await _orders.DeleteDetailFromOrder(orderId, detailId);
                return NoContent();
            }
            catch (EntityNotFoundException<OrderModel>)
            {
                //If the order is not found, return 404
                return NotFound();
            }
        }
    }
}
