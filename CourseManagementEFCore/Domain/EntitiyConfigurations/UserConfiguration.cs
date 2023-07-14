using CourseManagementEFCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseManagementEFCore.Domain.EntitiyConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(t => t.ID);

            builder.HasIndex(t => t.Username)
                .IsUnique();

            builder.Property(t => t.Email)
                .HasColumnType("nvarchar")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(t => t.Name)
                .HasColumnType("nvarchar")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(t => t.Password)
                .HasColumnType("nvarchar")
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(t => t.PhoneNumber)
                .HasColumnType("varchar")
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(t => t.Surname)
                .HasColumnType("nvarchar")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(t => t.Username)
                .HasColumnType("nvarchar")
                .HasMaxLength(50)
                .IsRequired();

            builder.HasOne(t => t.Role)
               .WithMany(t => t.User)
               .HasForeignKey(t => t.RoleID)
               .HasConstraintName("FK_Users_Roles")
               .IsRequired();
        }
    }
}
