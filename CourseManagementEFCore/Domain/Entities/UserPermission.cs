using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementEFCore.Domain.Entities
{
    public class UserPermission : BaseEntity<int>
    {
        public int UserID { get; set; }
        public User User { get; set; }
        public int PermissionID { get; set; }
        public Permission Permission { get; set; }
    }
}
