using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using fluttyBackend.Domain.Models.UserRoleEntities;
using fluttyBackend.Domain.Models.Utils;

namespace fluttyBackend.Domain.Models.Company.OnRequest
{
    [Table(EntityNamesConstants.CompanyAdditionRequests)]
    public class CompanyAdditionRequest
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        [Required]
        public string Photo { get; set; }

        // foreign key vvv
        [Required]
        public Guid OwnerUserId { get; set; }
        [ForeignKey(nameof(OwnerUserId))]
        public virtual User Founder { get; set; }
        // foreign key ^^^
    }
}