using CourseManagementEFCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseManagementEFCore.Domain.EntitiyConfigurations
{
    public class UserPermissionConfiguration : IEntityTypeConfiguration<UserPermission>
    {
        public void Configure(EntityTypeBuilder<UserPermission> builder)
        {
            builder.HasKey(t => t.ID);

            builder.HasOne(t => t.User)
               .WithMany(t => t.UserPermissions)
               .HasForeignKey(t => t.UserID)
               .HasConstraintName("FK_Users_UserPermission");

            builder.HasOne(t => t.Permission)
                .WithMany(t => t.UserPermissions)
                .HasForeignKey(t => t.PermissionID)
                .HasConstraintName("FK_Permissions_UserPermission");
        }
    }
}
