using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using fluttyBackend.Domain.Models.UserRoleEntities;
using fluttyBackend.Domain.Models.Utils;
using Microsoft.EntityFrameworkCore;

namespace fluttyBackend.Domain.Models.Company
{
    [Table(EntityNamesConstants.Company)]
    [Index(nameof(CompanyTbl.Name), IsUnique = true)]
    public class CompanyTbl
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Photo { get; set; }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        [MaxLength(300)]
        public string AboutCompany { get; set; }

        [Required]
        public bool Approved { get; set; } = false;

        [Required]
        public bool Blocked { get; set; } = false;

        // foreign key vvv
        public Guid FounderId { get; set; }
        [ForeignKey(nameof(FounderId))]
        public virtual User Founder { get; set; }
        // foreign key ^^^
    }
}