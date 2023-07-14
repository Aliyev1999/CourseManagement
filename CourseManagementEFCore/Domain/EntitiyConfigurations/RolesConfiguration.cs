using CourseManagementEFCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementEFCore.Domain.EntitiyConfigurations
{
    public class RolesConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            //primary key
            builder.HasKey(t => t.ID);

            builder.Property(t => t.Name)
                .HasColumnType("nvarchar")
                .HasMaxLength(150)
                .IsRequired();               
        }
    }
}
