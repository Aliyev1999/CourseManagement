
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementCore.Domain.Entities
{
    public class Course: BaseEntity<int>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Profession { get; set; }

        public decimal Prices { get; set; }

        public int Duration { get; set; }

        public int TeacherId { get; set; }

        public bool IsDeleted { get; set; }

        public int MaxStudentCount { get; set; }

        public ICollection<CourseStudent> CourseStudents { get; set; }

        public Teacher Teacher { get; set; }

        //TODO add meetin times
    }
}




//Course
//Student
//Teachers

//Users
//Permission
//Roles