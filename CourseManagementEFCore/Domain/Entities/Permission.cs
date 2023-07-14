using CourseManagementEFCore.Domain.Abstractions;
using CourseManagementEFCore.Domain.Abstractions.Interfaces;
using System.Collections.Generic;

namespace CourseManagementEFCore.Domain.Entities
{
    public class Permission : FullAuditProperty, ISoftDelete
    {
        public string Description { get; set; }
        public bool IsOkay { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<UserPermission> UserPermissions { get; set; }
    }
}
