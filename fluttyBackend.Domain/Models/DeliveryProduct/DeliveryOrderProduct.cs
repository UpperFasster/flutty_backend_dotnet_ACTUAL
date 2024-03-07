using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using fluttyBackend.Domain.Models.ProductEntities;
using fluttyBackend.Domain.Models.UserRoleEntities;
using fluttyBackend.Domain.Models.Utils;

namespace fluttyBackend.Domain.Models.DeliveryProduct
{
    [Table(EntityNamesConstants.DeliveryProduct)]
    public class DeliveryOrderProduct
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        public int Status { get; set; }

        [Required]
        [MaxLength(190)]
        public string Address { get; set; }

        [Required]
        [MaxLength(190)]
        public string Coordinate { get; set; }

        [Required]
        public bool Finished { get; set; } = false;

        [Required]
        public bool FullDelivered { get; set; } = false;

        [Required]
        [MaxLength(255)]
        public string FinishedCause { get; set; }

        [Required]
        public bool FullPrice { get; set; }

        // time stamp
        [Required]
        public int UtcWillFinishedTime { get; set; }

        // time stamp
        [Required]
        public int UtcAddedTime { get; set; }

        // time stamp
        [Required]
        public int UtcUpdatedTime { get; set; }

        // foreign key vvv
        [Required]
        public Guid UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }
        // foreign key ^^^
    }
}