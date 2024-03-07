using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using fluttyBackend.Domain.Models.Utils;
using Microsoft.EntityFrameworkCore;

namespace fluttyBackend.Domain.Models.ProductEntities.OnRequest
{
    [Table(EntityNamesConstants.ProductAdditionRequests)]
    [Index(nameof(ProductAdditionRequest.Name), IsUnique = true)]
    public class ProductAdditionRequest
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

        public List<string> AdditionalPhotos { get; set; }

        [Required]
        public double Price { get; set; }
    }
}