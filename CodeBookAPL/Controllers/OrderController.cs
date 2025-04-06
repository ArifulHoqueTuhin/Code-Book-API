using AutoMapper;
using CodeBookAPL.Models;
using CodeBookAPL.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodeBookAPL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly CodeBookContext _dbData;
        private readonly IMapper _mapper;

        public OrderController(CodeBookContext dbData, IMapper mapper)
        {
            _dbData = dbData;
            _mapper = mapper;
        }




        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDTOcs>> GetOrderById(int id)
        {
            var order = await _dbData.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.OrderId == id);

            if (order == null) return NotFound();

            return Ok(_mapper.Map<OrderDTOcs>(order));
        }


        [HttpPost]
        public async Task<ActionResult<OrderDTOcs>> CreateOrder([FromBody] OrderDTOcs orderDto)
        {
            if (orderDto == null || orderDto.OrderItems == null || !orderDto.OrderItems.Any())
            {
                return BadRequest("Order must include items.");
            }

            var order = new Order
            {
                UserId = orderDto.UserId,
                UserEmail = orderDto.UserEmail,
                AmountPaid = orderDto.AmountPaid,
                Quantity = orderDto.Quantity,
                OrderDate = orderDto.OrderDate,
                OrderItems = orderDto.OrderItems.Select(item => new OrderItem
                {
                    ProductId = item.ProductId,
                    Price = item.Price,
                    Quantity = item.Quantity
                }).ToList()
            };

            _dbData.Orders.Add(order);
            await _dbData.SaveChangesAsync();

            return Ok(_mapper.Map<OrderDTOcs>(order));
        }


        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<OrderDTOcs>>> GetOrdersByUserId(int userId)
        {
            var orders = await _dbData.Orders
                .Include(o => o.OrderItems)
                .Where(o => o.UserId == userId)
                .ToListAsync();

            if (orders == null || !orders.Any())
                return NotFound("No orders found for this user.");

            return Ok(_mapper.Map<List<OrderDTOcs>>(orders));
        }













    }
}
