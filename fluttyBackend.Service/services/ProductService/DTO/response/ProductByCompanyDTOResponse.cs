namespace fluttyBackend.Service.services.ProductService.DTO.response
{
    public class ProductByCompanyDTOResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public IEnumerable<string> AdditionalPhotos { get; set; }
        public double Price { get; set; }
        public double Rating { get; set; }
        public bool InProduction { get; set; }
        public bool Blocked { get; set; }
        public bool Verified { get; set; }
    }
}