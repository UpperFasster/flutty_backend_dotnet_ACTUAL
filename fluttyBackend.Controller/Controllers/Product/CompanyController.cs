using fluttyBackend.Controller.Controllers.Utils;
using fluttyBackend.Service.services.JwtService;
using fluttyBackend.Service.services.ProductService;
using fluttyBackend.Service.services.ProductService.DTO.request;
using fluttyBackend.Service.services.ProductService.DTO.response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace fluttyBackend.Controller.Controllers
{
    [ApiController]
    [Route(PathNameConstants.Company)]
    [Authorize]
    public class CompanyController : ControllerBase
    {
        private readonly IAsyncProductService productService;
        private readonly IAsyncJwtUtil jwtUtil;


        public CompanyController(
            IAsyncProductService productService,
            IAsyncJwtUtil jwtUtil)
        {
            this.productService = productService;
            this.jwtUtil = jwtUtil;
        }

        [HttpGet("{companyId}/getAll/")]
        public async Task<IEnumerable<ProductByCompanyDTOResponse>> Foo(
            [FromRoute] Guid companyId,
            [FromHeader(Name = "Authorization")] string authorizationHeader,
            [FromQuery] int page = 1,
            [FromQuery] int size = 10)
        {
            var userId = await jwtUtil.AsyncGetUserIdByAuthHeader(authorizationHeader);
            var res = await productService.GetProductListByCompany(page, size, userId);
            return await Task.FromResult(res);
        }

        [HttpPost("{companyId}/product/add")]
        public async Task<IActionResult> AddProduct(
            [FromRoute] Guid companyId,
            [FromForm] NewProductDTO newProductDTO,
            [FromForm(Name = "img")] IFormFile img,
            [FromForm(Name = "imgs")] List<IFormFile> images)
        {
            var result = await productService.AddAsync(companyId, newProductDTO, img, images);

            if (result == 1)
            {
                return Ok("Product added successfully");
            }

            return BadRequest("Failed to add product");
        }

        [HttpDelete("{companyId}/delete/{productId}")]
        public async Task<IActionResult> DeleteProduct(
            [FromRoute] Guid companyId,
            [FromRoute] Guid productId)
        {
            var result = await productService.DeleteByIdAsync(companyId, productId);

            if (result == 1)
            {
                return Ok("Product deleted successfully");
            }

            return BadRequest("Failed to delete product");
        }

        [HttpPut("{companyId}/update")]
        public async Task<IActionResult> UpdateProduct(
            [FromRoute] Guid companyId,
            [FromForm] UpdateProductDTO updateProductDTO,
            [FromForm] IFormFile img,
            [FromForm] List<IFormFile> images)
        {
            var result = await productService.UpdateAsync(companyId, updateProductDTO, img, images);

            if (result == 1)
            {
                return Ok("Product updated successfully");
            }

            if (result == -1)
            {
                return NotFound("Product not found");
            }

            return BadRequest("Failed to update product");
        }
        // --------------------------------------
    }
}