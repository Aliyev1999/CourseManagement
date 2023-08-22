using CourseManagementEFCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagement.Models
{
    public class UserViewModel : BaseViewModel
    {
        public string Password { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int RoleID { get; set; }
        public Role Role { get; set; }
        public ICollection<UserPermission> UserPermissions { get; set; } = new List<UserPermission>();
        public bool IsDeleted { get; set; }
        public List<UserViewModel> Users { get; set; } = new List<UserViewModel>();
        public IEnumerable<Role> Roles { get; set; } = new List<Role>();
        public PermissionViewModel Permissions { get; set; } = new PermissionViewModel();

    }
}
