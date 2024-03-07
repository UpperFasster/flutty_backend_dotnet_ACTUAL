using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using fluttyBackend.Domain.Models.UserRoleEntities;
using fluttyBackend.Domain.Models.Utils;

namespace fluttyBackend.Domain.Models.OnRequest
{
    [Table(EntityNamesConstants.TicketList)]
    public class TicketList
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid TicketId { get; set; }

        [Required]
        public int StatusCode { get; set; }

        [Required]
        public bool Closed { get; set; }

        [Required]
        public DateTime TicketCreatedAt;

        [Required]
        public DateTime TicketCreatedAtUTC;

        public DateTime TicketClosedAt;

        public DateTime TicketClosedAtUTC;

        // foreign key vvv
        [Required]
        public Guid TicketClosedBy { get; set; }
        [ForeignKey(nameof(TicketClosedBy))]
        public virtual User User { get; set; }
        // foreign key ^^^
    }
}