using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using fluttyBackend.Domain.Models.Company;
using fluttyBackend.Domain.Models.Utils;
using Microsoft.EntityFrameworkCore;

namespace fluttyBackend.Domain.Models.ProductEntities
{
    [Table(EntityNamesConstants.Product)]
    [Index(nameof(Product.Name), IsUnique = true)]
    public class Product
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        [MaxLength(300)]
        public string Description { get; set; }

        [Required]
        [MaxLength(length: 90)]
        public string Photo { get; set; }

        public virtual ICollection<OtMPhotosOfProduct> AdditionalPhotos { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public double Rating { get; set; } = 0.0;

        [Required]
        public bool InProduction { get; set; }

        [Required]
        public bool Blocked { get; set; } = false;

        [Required]
        public bool Verified { get; set; } = false;

        // foreign key vvv
        public Guid CompanyId { get; set; }
        [ForeignKey(nameof(CompanyId))]
        public virtual CompanyTbl Company { get; set; }
        // foreign key ^^^
    }
}