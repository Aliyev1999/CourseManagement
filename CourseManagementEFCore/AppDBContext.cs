using CourseManagementEFCore.Domain.Abstractions;
using CourseManagementEFCore.Domain.Entities;
using CourseManagementEFCore.Domain.EntitiyConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CourseManagementEFCore
{
    public class AppDBContext : DbContext
    {

        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }

        private static readonly MethodInfo ConfigureGlobalFiltersMethodInfo = typeof(AppDBContext)
                                                                              .GetMethod(nameof(ConfigureGlobalFilters), BindingFlags.Instance | BindingFlags.NonPublic);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CourseConfiguration());
            modelBuilder.ApplyConfiguration(new CourseStudentsConfiguration());
            modelBuilder.ApplyConfiguration(new StudentsConfiguration());
            modelBuilder.ApplyConfiguration(new TeachersConfiguration());
            modelBuilder.ApplyConfiguration(new RolesConfiguration());
            modelBuilder.ApplyConfiguration(new PermissionsConfiguratin());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserPermissionConfiguration());

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                ConfigureGlobalFiltersMethodInfo
                    .MakeGenericMethod(entityType.ClrType)
                    .Invoke(this, new object[] { modelBuilder, entityType });
            }
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseStudent> CourseStudents { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<IAudited>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        SetCreationAuditProperties(entry);
                        break;
                    case EntityState.Modified:
                        SetModificationAuditProperties(entry);
                        break;
                    case EntityState.Deleted:
                        CancelDeletionForSoftDelete(entry);
                        SetDeletionAuditProperties(entry);
                        break;
                }
            }

            int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return result;
        }

        protected void ConfigureGlobalFilters<TEntity>(ModelBuilder modelBuilder, IMutableEntityType entityType) where TEntity : class
        {
            if (ShouldFilterEntity<TEntity>(entityType))
            {
                var filterExpression = CreateFilterExpression<TEntity>();
                if (filterExpression != null)
                {
                    modelBuilder.Entity<TEntity>().HasQueryFilter(filterExpression);
                }
            }
        }

        protected virtual bool ShouldFilterEntity<TEntity>(IMutableEntityType entityType) where TEntity : class
        {
            if (typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity)))
            {
                return true;
            }

            return false;
        }

        protected virtual Expression<Func<TEntity, bool>> CreateFilterExpression<TEntity>() where TEntity : class
        {
            Expression<Func<TEntity, bool>> expression = null;

            if (typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity)))
            {
                Expression<Func<TEntity, bool>> softDeleteFilter = e => !((ISoftDelete)e).IsDeleted;
                expression = softDeleteFilter;
            }

            return expression;
        }

        protected virtual void SetCreationAuditProperties(EntityEntry entry)
        {
            if (!(entry.Entity is ICreationTime hasCreationTimeEntity)) return;

            if (hasCreationTimeEntity.CreationDate == default)
            {
                hasCreationTimeEntity.CreationDate = DateTime.Now;
            }

            if (!(entry.Entity is ICreationAudited creationAuditedEntity)) return;

            if (creationAuditedEntity.CreatorID != null)
            {
                //CreatedUserId is already set
                return;
            }

        }

        protected virtual void SetModificationAuditProperties(EntityEntry entry)
        {
            if (!(entry.Entity is IModificationTime modificationDateEntity)) return;

            if (modificationDateEntity.LastModificationDate == default)
            {
                modificationDateEntity.LastModificationDate = DateTime.Now;
            }

            if (!(entry.Entity is IModificationAudited modificationAuditedEntity)) return;

            if (modificationAuditedEntity.LastModificatorID != null)
            {
                //LastModifiedUserId is already set
                return;
            }

        }

        protected virtual void SetDeletionAuditProperties(EntityEntry entry)
        {

            if (!(entry.Entity is IDeletionTime hasDeletionTimeEntity)) return;

            if (hasDeletionTimeEntity.DeletedDate == default)
            {
                hasDeletionTimeEntity.DeletedDate = DateTime.Now;
            }

            if (!(entry.Entity is IDeletionAudited deletionAuditedEntity)) return;

            deletionAuditedEntity.DeletedDate = DateTime.Now;
        }

        protected virtual void CancelDeletionForSoftDelete(EntityEntry entry)
        {
            if (!(entry.Entity is ISoftDelete))
            {
                return;
            }

            entry.Reload();
            entry.State = EntityState.Modified;
            ((ISoftDelete)entry.Entity).IsDeleted = true;
        }
    }
}
