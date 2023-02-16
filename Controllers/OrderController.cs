using DemoAPI.Migrations;
using DemoAPI.Models.Data;
using DemoAPI.Models.DTOs;
using DemoAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DemoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository) => _orderRepository = orderRepository;

        [HttpGet("{id}")]
        [ActionName(nameof(GetById))]
        public ActionResult<Order> GetById(int id)
        {
            try
            {
                if (id == null) return NotFound();
                var order = _orderRepository.GetById(id);
                if (order == null) return NotFound("The order does not exist");
                return order;

            } catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [ActionName(nameof(CreateOrder))]
        public async Task<IResult> CreateOrder(int user_id, OrderDTO order)
        {
            try
            {
                if (order == null) return Results.NotFound();
                var Order = new Order();
                Order.Total = order.Total;
                Order.User_id = user_id;
                Order.Created_at = DateTime.Now;
                Order.Updated_at = DateTime.Now;
                var createdOrder = await _orderRepository.Create(Order);
                return Results.Ok(createdOrder);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
