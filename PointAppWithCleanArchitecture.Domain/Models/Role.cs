using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointAppWithCleanArchitecture.Domain.Models
{
    public class Role : IdentityRole
    {
        public ICollection<UserRole> UserRoles { get; set; }

        public const string Admin = "Admin";
        public const string User = "User";
        public const string Customer = "Customer";


    }
}
