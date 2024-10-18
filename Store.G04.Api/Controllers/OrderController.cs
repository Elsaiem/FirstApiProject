using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.G04.Api.Attributes;
using Store.G04.Api.Errors;
using Store.G04.Core;
using Store.G04.Core.Dtos.Orders;
using Store.G04.Core.Entities.Orderr;
using Store.G04.Core.ServicesContract;
using System.Security.Claims;

namespace Store.G04.Api.Controllers
{
    
    public class OrderController :BaseApiController
        
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public OrderController(IOrderService orderService,IMapper mapper ,IUnitOfWork unitOfWork )
        {
            _orderService = orderService;
            this._mapper = mapper;
            this._unitOfWork = unitOfWork;
        }
        [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> CreateOrder(OrdersDto ordersDto)
        {
            var userEmail= User.FindFirstValue(ClaimTypes.Email);
            if(userEmail is null) return Unauthorized(new ApiErrorResponse(StatusCodes.Status401Unauthorized));

            var shipAddres =  _mapper.Map<AddressOrder>(ordersDto.ShipAddress);
            var order = await _orderService.CreateOrderAsync(userEmail, ordersDto.BusketId, ordersDto.DeliveryMethodId, shipAddres);
            if (order is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
                
            return  Ok( _mapper.Map<OrderToReturnDto>(order));
                
        }
        [Authorize]  
        [CachedAttribute(300)]
        [HttpGet("GetAllOrders")]
        public async Task<IActionResult> GetOrdersForSpecificUser()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (userEmail is null) return Unauthorized(new ApiErrorResponse(StatusCodes.Status401Unauthorized));
            var orders = await _orderService.GetOrdersForSpecifiUserAsync(userEmail);
            if (orders is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
            return Ok(_mapper.Map<IEnumerable<OrderToReturnDto>>(orders));
        }
    
    
        [Authorize]
        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderIdForSpecificUser(int orderId)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (userEmail is null) return Unauthorized(new ApiErrorResponse(StatusCodes.Status401Unauthorized));
            var order = await _orderService.GetOrderIdForSpecifiUserAsync(userEmail,orderId);
            if (order is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
            return Ok(_mapper.Map<OrderToReturnDto>(order));
        }
       
        [HttpGet("DeliveryMethods")]
        public async Task<IActionResult> GetDeliveryMethods()
        {

            var deliveryMehtods = await _unitOfWork.Repository<DeliveryMethod,int>().GetAllAsync();
            if (deliveryMehtods is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
            return Ok(deliveryMehtods);
        }




    }



    
}
