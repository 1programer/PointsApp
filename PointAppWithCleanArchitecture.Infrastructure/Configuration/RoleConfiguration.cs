using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PointAppWithCleanArchitecture.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointAppWithCleanArchitecture.Infrastructure.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.Property(r => r.Name).IsRequired();
            builder.Property(r => r.NormalizedName).IsRequired();

            builder.HasData(
                new Role { Id = "1", Name = Role.Customer, NormalizedName = Role.Customer.ToUpper() },
                new Role { Id = "2", Name = Role.User, NormalizedName = Role.User.ToUpper() },
                new Role { Id = "3", Name = Role.Admin, NormalizedName = Role.Admin.ToUpper() }
            );

        }
    }
}
