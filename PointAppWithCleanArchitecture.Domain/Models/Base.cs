using System.ComponentModel.DataAnnotations;

namespace PointAppWithCleanArchitecture.Domain.Models
{
    public abstract class Base
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public DateTime DateOfCreate { get; set; }
        [Required]
        public DateTime DateOfUpdate { get; set; }
    }
}
