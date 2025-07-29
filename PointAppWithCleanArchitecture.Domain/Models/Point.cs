using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PointAppWithCleanArchitecture.Domain.Models
{
    public class Point : Base
    {
        public required decimal Amount { get; set; }
        [Required, ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public required bool IsRedeemed { get; set; }
    }
}
