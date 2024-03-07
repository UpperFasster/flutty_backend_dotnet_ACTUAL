using fluttyBackend.Domain.Models.ProductEntities;

namespace fluttyBackend.Service.services.DeliverySystemService.DTO.response
{
    public class ProductOrderInfoDTO
    {
        public Guid DeliveryId { get; set; }
        public Guid ProductId { get; set; }
        public double ByPrice { get; set; }   
        public bool Finished { get; set; }
        public Product Product { get; set; }   
    }
}