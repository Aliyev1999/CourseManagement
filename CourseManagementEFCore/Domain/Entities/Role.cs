using CourseManagementEFCore.Domain.Abstractions;
using CourseManagementEFCore.Domain.Abstractions.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementEFCore.Domain.Entities
{
    public class Role : FullAuditProperty, ISoftDelete
    {
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<User> User { get; set; }
    }
}
