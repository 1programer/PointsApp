using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PointAppWithCleanArchitecture.Domain.Models
{
    public class Point : Base
    {
        [Required]
        public decimal Amount { get; set; }
        [Required, ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        [Required]
        public bool IsRedeemed { get; set; }
    }
}
