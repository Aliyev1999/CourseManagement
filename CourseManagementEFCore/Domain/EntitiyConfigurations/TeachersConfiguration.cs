using CourseManagementEFCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseManagementEFCore.Domain.EntitiyConfigurations
{
    public class TeachersConfiguration : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.HasKey(t => t.ID);

            builder.HasMany(t => t.Courses)
                .WithOne(t => t.Teacher)
                .HasForeignKey(t => t.TeacherID)
                .HasConstraintName("FK_Teachers_Courses")
                .IsRequired();

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
