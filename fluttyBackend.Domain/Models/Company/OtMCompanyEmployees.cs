using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using fluttyBackend.Domain.Models.UserRoleEntities;
using fluttyBackend.Domain.Models.Utils;
using Microsoft.EntityFrameworkCore;

namespace fluttyBackend.Domain.Models.Company
{
    [Table(EntityNamesConstants.OtMCompanyEmployees)]
    public class OtMCompanyEmployees
    {
        [Key]
        // foreign key vvv
        public Guid EmployeeId { get; set; }
        [ForeignKey(nameof(EmployeeId))]
        public virtual User Employee { get; set; }

        [Required]
        public Guid CompanyId { get; set; }
        [ForeignKey(nameof(CompanyId))]
        public virtual CompanyTbl Company { get; set; }
        // foreign key ^^^
    }
}