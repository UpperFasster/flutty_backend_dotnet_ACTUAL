using fluttyBackend.Controller.Controllers.Utils;
using fluttyBackend.Service.services.ProductService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace fluttyBackend.Controller.Controllers;

[ApiController]
[Route(PathNameConstants.Product)]
[Authorize]
public class ProductController : ControllerBase
{
    private readonly IAsyncProductService productService;

    public ProductController(IAsyncProductService productService)
    {
        this.productService = productService;
    }

    [HttpGet("getAll")]
    public async Task<IActionResult> GetProductPagination(
        [FromQuery] int page = 1,
        [FromQuery] int size = 10)
    {
        if (page <= 0)
        {
            page = 1;
        }

        if (size <= 0 || size > 10)
        {
            size = 10;
        }

        var products = await productService.getProductPagination(page, size);
        return Ok(products);
    }

    [HttpGet("getRecomendation")]
    public async Task<bool> GetRecomendation()
    {
        return await Task.FromResult(true);
    }
}
