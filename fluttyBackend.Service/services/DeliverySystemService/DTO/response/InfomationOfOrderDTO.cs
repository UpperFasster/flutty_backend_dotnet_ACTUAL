namespace fluttyBackend.Service.services.DeliverySystemService.DTO.response
{
    public class InfomationOfOrderDTO
    {
        public Guid Id { get; set; }
        public int OrderId { get; set; }
        public int WillBeReadyUtcTime { get; set; }
        public double Price { get; set; }
        public string Address { get; set; }
        public string Coordinate { get; set; }
        public int Status { get; set; }
        public string Photo { get; set; }
    }
}