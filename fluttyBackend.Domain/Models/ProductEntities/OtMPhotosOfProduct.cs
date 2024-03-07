using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using fluttyBackend.Domain.Models.Utils;
using Microsoft.EntityFrameworkCore;

namespace fluttyBackend.Domain.Models.ProductEntities
{
    [Table(EntityNamesConstants.OtMPhotoOfProduct)]
    [Index(nameof(OtMPhotosOfProduct.FileName), IsUnique = true)]
    public class OtMPhotosOfProduct
    {
        [Key]
        public Guid PhotoId { get; set; }

        [Required]
        [MaxLength(length: 150)]
        public string FileName { get; set; }

        // foreign key vvv
        [Required]
        public Guid ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; }
        // foreign key ^^^
    }
}