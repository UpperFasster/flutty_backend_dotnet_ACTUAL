using fluttyBackend.Controller.Controllers.Utils;
using fluttyBackend.Service.services.DeliverySystemService;
using fluttyBackend.Service.services.DeliverySystemService.DTO.response;
using fluttyBackend.Service.services.JwtService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace fluttyBackend.Controller.Controllers.Orders
{
    [ApiController]
    [Route(PathNameConstants.Delivery)]
    [Authorize]
    public class DeliverySystemController : ControllerBase
    {
        private readonly IAsyncDeliverySystemService deliverySystemService;
        private readonly IAsyncJwtUtil jwtUtil;

        public DeliverySystemController(
            IAsyncDeliverySystemService asyncDeliverySystemService,
            IAsyncJwtUtil jwtUtil)
        {
            this.deliverySystemService = asyncDeliverySystemService;
            this.jwtUtil = jwtUtil;
        }

        [HttpGet("getActualOrders")]
        public async Task<IEnumerable<InfomationOfOrderDTO>> GetActualOrders(
            [FromHeader(Name = "Authorization")] string authorizationHeader,
            [FromQuery] int page = 1,
            [FromQuery] int size = 10)
        {
            var userId = await jwtUtil.AsyncGetUserIdByAuthHeader(authorizationHeader);
            return await deliverySystemService.GetActualOrdersAsync(userId, page, size);
        }

        [HttpGet("getCompletedOrders")]
        public async Task<IEnumerable<InfomationOfOrderDTO>> GetCompletedOrders(
            [FromHeader(Name = "Authorization")] string authorizationHeader,
            [FromQuery] int page = 1,
            [FromQuery] int size = 10)
        {
            var userId = await jwtUtil.AsyncGetUserIdByAuthHeader(authorizationHeader);
            return await deliverySystemService.GetCompletedOrdersAsync(userId, page, size);
        }

        [HttpGet("add")]
        public async Task AddItemToCart(
            [FromHeader(Name = "Authorization")] string authorizationHeader)
        {
            await Task.Delay(100);
        }

        [HttpGet("delete")]
        public async Task RemoveItemFromCart(
            [FromHeader(Name = "Authorization")] string authorizationHeader)
        {
            await Task.Delay(100);
        }
    }
}