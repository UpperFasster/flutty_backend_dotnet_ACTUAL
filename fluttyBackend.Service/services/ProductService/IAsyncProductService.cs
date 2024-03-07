using fluttyBackend.Domain.Models;
using fluttyBackend.Domain.Models.ProductEntities;
using fluttyBackend.Service.services.ProductService.DTO.request;
using fluttyBackend.Service.services.ProductService.DTO.response;
using Microsoft.AspNetCore.Http;

namespace fluttyBackend.Service.services.ProductService
{
    public interface IAsyncProductService
    {
        Task<int> AddAsync(Guid companyId, NewProductDTO newProductDTO, IFormFile img, List<IFormFile> images);
        Task<IEnumerable<Product>> getProductPagination(int page, int size);
        Task<int> DeleteByIdAsync(Guid companyId, Guid productId);
        Task<int> UpdateAsync(Guid companyId, UpdateProductDTO updateProductDTO, IFormFile img, List<IFormFile> images);
        Task<IEnumerable<ProductByCompanyDTOResponse>> GetProductListByCompany(int page, int size, Guid userId);
    }
}
