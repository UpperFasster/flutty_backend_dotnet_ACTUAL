using fluttyBackend.Controller.Controllers.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace fluttyBackend.Controller.Controllers.Orders
{
    [ApiController]
    [Route(PathNameConstants.CartItem)]
    [Authorize]
    public class CartItemController : ControllerBase
    {
        [HttpGet("getAll")]
        public async Task GetAll(
            [FromHeader(Name = "Authorization")] string authorizationHeader,
            [FromQuery] int page = 1,
            [FromQuery] int size = 10)
        {
            await Task.Delay(100);
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