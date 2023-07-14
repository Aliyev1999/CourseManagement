using CourseManagementEFCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseManagementEFCore.Domain.EntitiyConfigurations
{
    public class PermissionsConfiguratin : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.HasKey(t => t.ID);

            builder.Property(t => t.Description)
                .HasColumnType("nvarchar")
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
