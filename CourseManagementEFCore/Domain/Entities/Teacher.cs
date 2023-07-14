using CourseManagementEFCore.Domain.Abstractions;
using CourseManagementEFCore.Domain.Abstractions.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementEFCore.Domain.Entities
{
    public class Teacher : FullAuditProperty, ISoftDelete
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public string Profession { get; set; }
        public ICollection<Course> Courses { get; set; }
        public bool IsDeleted { get; set; }
    }
}
