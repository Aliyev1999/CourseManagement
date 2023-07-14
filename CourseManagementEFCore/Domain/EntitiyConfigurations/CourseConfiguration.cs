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
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(t => t.ID);

            builder.Property(t => t.Description)
                .HasColumnType("nvarchar")
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(t => t.Duration)
                .HasColumnType("bigint")
                .IsRequired();
        }
    }
}