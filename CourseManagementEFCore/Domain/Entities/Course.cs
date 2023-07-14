using CourseManagementEFCore.Domain.Abstractions;
using CourseManagementEFCore.Domain.Abstractions.Interfaces;
using System.Collections.Generic;

namespace CourseManagementEFCore.Domain.Entities
{
    public class Course : FullAuditProperty, ISoftDelete
    {
        public string Description { get; set; }
        public string Name { get; set; }
        public decimal Prices { get; set; }
        public string Profession { get; set; }
        public int Duration { get; set; }
        public int TeacherID { get; set; }
        public Teacher Teacher { get; set; }
        public ICollection<CourseStudent> CourseStudents { get; set; }
        public bool IsDeleted { get; set; }
    }
}
