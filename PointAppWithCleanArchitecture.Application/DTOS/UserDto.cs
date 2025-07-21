using System.ComponentModel.DataAnnotations;

namespace PointAppWithCleanArchitecture.Application.DTOS
{
    public class UserDto
    {
        [Required, MaxLength(20)]
        public string Username { get; set; }
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
        [Required, MaxLength(25)]
        public string Password { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public int Points { get; set; }

    }


}
