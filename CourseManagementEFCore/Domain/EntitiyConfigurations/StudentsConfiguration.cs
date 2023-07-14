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
    public class StudentsConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(t => t.ID);

            builder.Property(t => t.Email)
                 .HasColumnType("nvarchar")
                 .HasMaxLength(100)
                 .IsRequired();

            builder.Property(t => t.BirthDate)
                .HasColumnType("date")
                .IsRequired();

            builder.Property(t => t.Name)
                .HasColumnType("nvarchar")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(t => t.PhoneNumber)
                .HasColumnType("varchar")
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(t => t.Surname)
                .HasColumnType("nvarchar")
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
