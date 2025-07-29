using System.ComponentModel.DataAnnotations;

namespace PointAppWithCleanArchitecture.Domain.Models
{
    public abstract class Base
    {
        public required Guid Id { get; set; }
        public required DateTime DateOfCreate { get; set; }
        public required DateTime DateOfUpdate { get; set; }
    }
}
