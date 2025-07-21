using System.ComponentModel.DataAnnotations;

namespace PointAppWithCleanArchitecture.Application.DTOS
{ 
    public class ItemDto
    {
        [Required, MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required,]
        public string ImgUrl { get; set; }
        [Required]
        public decimal Price { get; set; }
    }
}
