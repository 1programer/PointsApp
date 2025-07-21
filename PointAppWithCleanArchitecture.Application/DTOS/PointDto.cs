using PointAppWithCleanArchitecture.Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PointAppWithCleanArchitecture.Application.DTOS
{
    public class PointDto
    {
        [Required]
        public decimal amount { get; set; }
        [Required, ForeignKey(nameof(User))]
        public string UserId { get; set; }
        [Required]
        public bool IsRedeemed { get; set; }
    }
}
