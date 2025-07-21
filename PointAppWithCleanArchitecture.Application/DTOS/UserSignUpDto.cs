using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointAppWithCleanArchitecture.Application.DTOS
{
    public class UserSignUpDto
    {
        [Required] 
        public string Email { get; set; }
        [Required] 
        public string UserName { get; set; }

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
    }
}
