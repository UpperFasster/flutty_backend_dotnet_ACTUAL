using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using fluttyBackend.Domain.Models.ProductEntities;
using fluttyBackend.Domain.Models.UserRoleEntities;
using fluttyBackend.Domain.Models.Utils;

namespace fluttyBackend.Domain.Models.CartItem
{
    [Table(EntityNamesConstants.CartItem)]
    public class CartItem
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public int Count { get; set; }

        [Required]
        public int UtcAddedTime { get; set; }

        [Required]
        public int UtcUpdatedTime { get; set; }

        // foreign key vvv
        [Required]
        public Guid ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; }

        [Required]
        public Guid UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }
        // foreign key ^^^
    }
}