using System.ComponentModel.DataAnnotations;

namespace PointAppWithCleanArchitecture.Domain.Models
{
    public class Item : Base
    {
        [Required, MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string ImgUrl { get; set; }
        [Required]
        public decimal Price { get; set; }
    }
}
