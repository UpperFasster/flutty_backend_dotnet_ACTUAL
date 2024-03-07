using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using fluttyBackend.Domain.Models.ProductEntities;
using fluttyBackend.Domain.Models.Utils;

namespace fluttyBackend.Domain.Models.DeliveryProduct
{
    [Table(EntityNamesConstants.OtMDeliveryProduct)]
    public class OtMOrderProduct
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public double ByPrice { get; set; }

        [Required]
        public bool Finished { get; set; } = false;

        [Required]
        public bool Delivered { get; set; } = false;

        // time stamp
        public int UTCFinishedAt { get; set; }

        // time stamp
        [Required]
        public int UtcAddedTime { get; set; }

        // time stamp
        [Required]
        public int UtcUpdatedTime { get; set; }

        // foreign key vvv
        [Required]
        public Guid DeliveryOrderId { get; set; }
        [ForeignKey(nameof(DeliveryOrderId))]
        public virtual DeliveryOrderProduct DeliveryProduct { get; set; }


        [Required]
        public Guid ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; }
        // foreign key ^^^
    }
}