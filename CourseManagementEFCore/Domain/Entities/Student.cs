using CourseManagementEFCore.Domain.Abstractions;
using CourseManagementEFCore.Domain.Abstractions.Interfaces;
using System;
using System.Collections.Generic;

namespace CourseManagementEFCore.Domain.Entities
{
    public class Student : FullAuditProperty, ISoftDelete
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public ICollection<CourseStudent> CourseStudents { get; set; }
        public bool IsDeleted { get ; set ; }
    }
}
