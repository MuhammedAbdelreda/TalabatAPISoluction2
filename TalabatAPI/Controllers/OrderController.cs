using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat;
using TalabatAPI.Controllers;

namespace TalabatAPI
{
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }
        //string buyerEmail,string Id,int deliveryMethodId,OrderAddress shipToAddress
        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var orderAddress = _mapper.Map<AddressDto, OrderAddress>(orderDto.ShipToAddress);
            var result = _orderService.CreateOrder(buyerEmail, orderDto.Id, orderDto.DeliveryMethod, orderAddress);
            if (result == null) return BadRequest();
            return Ok(result);

        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Order>>> GetOrdersForUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _orderService.GetOrdersForUserAsync(email);
            return Ok(user);
        }

        [HttpGet("{id}")] //api/orders/{id}
        public async Task<ActionResult<Order>> GetOrderForUser(int id)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _orderService.GetOrdersByIdForUserAsync(id, email);
            return Ok(user);

        }

        [HttpGet("deliveryMethod")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethodsWithAsync()
        {
            var result = await _orderService.GetDeliveryMethodsAsync();
            return Ok(result);
        }
    }
    
}
