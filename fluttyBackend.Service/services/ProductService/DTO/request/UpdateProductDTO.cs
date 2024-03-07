namespace fluttyBackend.Service.services.ProductService.DTO.request
{
    public class UpdateProductDTO
    {
        public Guid Id { get; init; }
        public string Name { get; set; }

        public string Description { get; set; }

        public string Photo { get; set; }
        public HashSet<string> AdditionalPhotos { get; set; } = new HashSet<string>();
        public double Price { get; set; }
    }
}