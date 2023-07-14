using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementEFCore.Domain.Entities
{
    //Many to many relationships
    public class CourseStudent : BaseEntity<int>
    {
        public int CourseID { get; set; }
        public Course Course { get; set; }
        public int StudentID { get; set; }
        public Student Student { get; set; }
    }
}
