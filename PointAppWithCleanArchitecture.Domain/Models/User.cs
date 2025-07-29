using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace PointAppWithCleanArchitecture.Domain.Models
{
    public class User : IdentityUser
    {
        [MaxLength(20)]
        public required string Name { get; set; }
        [MaxLength(20)]
        public required string LName { get; set; }
        [MaxLength(20)]
        public required string Phone { get; set; }
        [MaxLength(20)]
        public required string PCode { get; set; }
        [MaxLength(20)]
        public required string PNumber { get; set; }
        public required DateTime BirthDate { get; set; }
        public required Decimal Points { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }

    }
}
