using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace PointAppWithCleanArchitecture.Domain.Models
{
    public class User : IdentityUser
    {
        [Required, MaxLength(20)]
        public string Name { get; set; }
        [Required, MaxLength(20)]
        public string LName { get; set; }
        [Required, MaxLength(20)]
        public string Phone { get; set; }
        [Required, MaxLength(20)]
        public string PCode { get; set; }
        [Required, MaxLength(20)]

        public string PNumber { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public Decimal Points { get; set; }

    }
}
