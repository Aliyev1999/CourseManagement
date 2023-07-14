using CourseManagementEFCore.Domain.Abstractions;
using CourseManagementEFCore.Domain.Abstractions.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementEFCore.Domain.Entities
{
    public class User : FullAuditProperty, ISoftDelete
    {
        public string Password { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int RoleID { get; set; }
        public Role Role { get; set; }
        public ICollection<UserPermission> UserPermissions { get; set; }
        public bool IsDeleted { get; set; }
    }
}
