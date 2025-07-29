using System.ComponentModel.DataAnnotations;

namespace PointAppWithCleanArchitecture.Domain.Models
{
    public class Item : Base
    {
        [MaxLength(50)]
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string ImgUrl { get; set; }
        public required decimal Price { get; set; }
    }
}
