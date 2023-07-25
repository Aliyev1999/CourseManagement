using CourseManagementEFCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagement.Models
{
    public class StudentViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public ICollection<CourseStudent> CourseStudents { get; set; }
        public bool IsDeleted { get; set; }
        public List<StudentViewModel> Students { get; set; } 
        public int CourseID { get; set; }
    }
}
