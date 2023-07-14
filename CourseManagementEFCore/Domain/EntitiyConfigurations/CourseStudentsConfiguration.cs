using CourseManagementEFCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseManagementEFCore.Domain.EntitiyConfigurations
{
    public class CourseStudentsConfiguration : IEntityTypeConfiguration<CourseStudent>
    {
        public void Configure(EntityTypeBuilder<CourseStudent> builder)
        {
            builder.HasKey(t => t.ID);

            builder.HasOne(t => t.Course)
                .WithMany(t => t.CourseStudents)
                .HasForeignKey(t => t.CourseID)
                .HasConstraintName("FK_Course_CourseStudent");

            builder.HasOne(t => t.Student)
                .WithMany(t => t.CourseStudents)
                .HasForeignKey(t => t.StudentID)
                .HasConstraintName("FK_Students_CourseStudent");
        }
    }
}
