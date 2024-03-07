using Dapper;
using fluttyBackend.Domain.Models.Company;
using fluttyBackend.Domain.Models.ProductEntities;
using fluttyBackend.Domain.Models.Utils;
using fluttyBackend.Domain.Repository.GenericRepository;
using fluttyBackend.Service.HardCodeStrings;
using fluttyBackend.Service.services.AuthService.roleVerifier;
using fluttyBackend.Service.services.JwtService;
using fluttyBackend.Service.services.PhotoService;
using fluttyBackend.Service.services.ProductService.DTO.request;
using fluttyBackend.Service.services.ProductService.DTO.response;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace fluttyBackend.Service.services.ProductService
{
    public class AsyncProductService : IAsyncProductService
    {
        private readonly IAsyncPhotoService photoService;
        private readonly IRepository<Product> productRepository;
        private readonly IAsyncRoleVerifierService roleVerifier;
        private readonly string connectionString;
        private readonly IAsyncJwtService jwtService;
        private readonly ILogger<AsyncProductService> logger;

        public AsyncProductService(
            IAsyncPhotoService photoService,
            IRepository<Product> productRepository,
            IAsyncRoleVerifierService roleVerifier,
            IConfiguration configuration,
            IAsyncJwtService jwtService,
            ILogger<AsyncProductService> logger)
        {
            this.photoService = photoService;
            this.productRepository = productRepository;
            this.roleVerifier = roleVerifier;
            connectionString = connectionString = configuration.GetConnectionString(
                ConnectionStringNames.DataBaseConnectionString
            );
            this.jwtService = jwtService;
            this.logger = logger;
        }

        public async Task<IEnumerable<Product>> getProductPagination(int page, int size)
        {
            return await productRepository.GetAllAsync(page, size);
        }

        public async Task<IEnumerable<ProductByCompanyDTOResponse>> GetProductListByCompany(
            int page,
            int size,
            Guid userId)
        {
            if (page <= 0)
            {
                page = 1;
            }

            if (size <= 0 || size > 10)
            {
                size = 10;
            }

            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();

                string sqlQuery = $@"
                    SELECT
                        p.""{nameof(Product.Id)}"",
                        p.""{nameof(Product.Name)}"",
                        p.""{nameof(Product.Description)}"",
                        p.""{nameof(Product.Photo)}"",
                        ARRAY_AGG(pp.""{nameof(OtMPhotosOfProduct.FileName)}"") AS ""{nameof(ProductByCompanyDTOResponse.AdditionalPhotos)}"",
                        p.""{nameof(Product.Price)}"",
                        p.""{nameof(Product.Rating)}"",
                        p.""{nameof(Product.InProduction)}"",
                        p.""{nameof(Product.Blocked)}"",
                        p.""{nameof(Product.Verified)}""
                    FROM
                        ""{EntityNamesConstants.Product}"" p
                    JOIN
                        ""{EntityNamesConstants.Company}"" c ON p.""{nameof(Product.CompanyId)}"" = c.""{nameof(CompanyTbl.Id)}""
                    LEFT JOIN
                        ""{EntityNamesConstants.OtMPhotoOfProduct}"" pp ON p.""{nameof(Product.Id)}"" = pp.""{nameof(OtMPhotosOfProduct.ProductId)}""
                    WHERE
                        EXISTS (
                            SELECT 1
                            FROM ""{EntityNamesConstants.OtMCompanyEmployees}"" ce
                            WHERE (ce.""{nameof(OtMCompanyEmployees.CompanyId)}"" = c.""{nameof(CompanyTbl.Id)}"" AND ce.""{nameof(OtMCompanyEmployees.EmployeeId)}"" = @UserId)
                            OR (c.""{nameof(CompanyTbl.FounderId)}"" = @UserId)
                        )
                    GROUP BY
                        p.""{nameof(Product.Id)}""
                    ORDER BY
                        p.""{nameof(Product.Id)}""
                    LIMIT
                        @size
                    OFFSET
                        (@page - 1) * @size;
                ";

                var result = await connection.QueryAsync<ProductByCompanyDTOResponse>(sqlQuery, new { UserId = userId, size, page });

                return result;
            }
        }

        public async Task<int> AddAsync(
            Guid companyId,
            NewProductDTO newProductDTO,
            IFormFile img,
            List<IFormFile> images)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    var photoId = Guid.NewGuid();
                    var fileName = await photoService.SavePhotoAsync(img, photoId);
                    var additionalPhotosId = new List<string>();
                    try
                    {
                        var productQuery = $@"
                            INSERT INTO ""{EntityNamesConstants.Product}"" (
                                ""{nameof(Product.Id)}"", ""{nameof(Product.Name)}"", ""{nameof(Product.Description)}"", 
                                ""{nameof(Product.CompanyId)}"", ""{nameof(Product.Photo)}"", ""{nameof(Product.Price)}"", 
                                ""{nameof(Product.Rating)}"", ""{nameof(Product.InProduction)}"", ""{nameof(Product.Blocked)}"", 
                                ""{nameof(Product.Verified)}"")
                            VALUES (
                                @Id, @Name, @Description, @CompanyId, @Photo, @Price, 
                                @Rating, @InProduction, @Blocked, @Verified
                            )";

                        await connection.ExecuteAsync(productQuery, new
                        {
                            Id = photoId,
                            Name = newProductDTO.Name,
                            Description = newProductDTO.Description,
                            CompanyId = companyId,
                            Photo = fileName,
                            Price = newProductDTO.Price,
                            Rating = 0.0,
                            InProduction = true,
                            Blocked = false,
                            Verified = false
                        }, transaction);

                        if (images != null && images.Any())
                        {
                            var additionalPhotosQuery = $@"
                                INSERT INTO ""{EntityNamesConstants.OtMPhotoOfProduct}""(
                                    ""{nameof(OtMPhotosOfProduct.PhotoId)}"", 
                                    ""{nameof(OtMPhotosOfProduct.FileName)}"", 
                                    ""{nameof(OtMPhotosOfProduct.ProductId)}"")
                                VALUES (@PhotoId, @FileName, @ProductId)";

                            foreach (var additionalImg in images)
                            {
                                var PhotoId = Guid.NewGuid();
                                var additionalFilename = await photoService.SavePhotoAsync(additionalImg, PhotoId);
                                additionalPhotosId.Add(additionalFilename);
                                await connection.ExecuteAsync(additionalPhotosQuery, new
                                {
                                    PhotoId = PhotoId,
                                    FileName = additionalFilename,
                                    ProductId = photoId
                                }, transaction);
                            }
                        }

                        transaction.Commit();
                        return 1;
                    }
                    catch (Exception e)
                    {

                        transaction.Rollback();
                        await photoService.DeletePhoto(fileName);
                        foreach (var file in additionalPhotosId)
                        {
                            await photoService.DeletePhoto(file);
                        }
                        var haha = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
                        logger.LogError($@"
                            {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} UTC - ""{e.Message}"".\n
                            Count of additionals photos ({images.Count}), created and deleted: ({additionalPhotosId.Count})");
                        throw;
                    }
                }
            }
        }

        public async Task<int> DeleteByIdAsync(Guid companyId, Guid productId)
        {
            if (await productRepository.RemoveAsync(productId))
            {
                return 1;
            }
            return 0;
        }

        public async Task<int> UpdateAsync(Guid companyId,
            UpdateProductDTO updateProductDTO,
            IFormFile img,
            List<IFormFile> images)
        {
            // Product existingProduct = await productRepository.GetByIdAsync(updateProductDTO.Id);

            // if (existingProduct == null)
            // {
            //     return -1;
            // }

            // existingProduct.Name = updateProductDTO.Name;
            // existingProduct.Description = updateProductDTO.Description;
            // existingProduct.Price = updateProductDTO.Price;

            // if (!string.IsNullOrEmpty(updateProductDTO.Photo) && img != null)
            // {
            //     await photoService.DeletePhoto(existingProduct.Photo);

            //     existingProduct.Photo = await photoService.SavePhotoAsync(img, existingProduct.Id);
            // }

            // if (updateProductDTO.AdditionalPhotos != null && updateProductDTO.AdditionalPhotos.Any())
            // {
            //     var existingAdditionalPhotoNames = existingProduct.AdditionalPhotos;

            //     var commonPhotoNames = updateProductDTO.AdditionalPhotos.Intersect(existingAdditionalPhotoNames);

            //     foreach (var commonPhotoName in commonPhotoNames)
            //     {
            //         await photoService.DeletePhoto(commonPhotoName);
            //     }

            //     foreach (var additionalPhoto in images)
            //     {
            //         await photoService.SavePhotoAsync(additionalPhoto, existingProduct.Id);
            //     }
            // }

            // await productRepository.UpdateAsync(existingProduct);
            return await Task.FromResult(1);
        }
    }
}