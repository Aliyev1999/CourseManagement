using CourseManagement.Enums;
using CourseManagementEFCore.Domain.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CourseManagement.Models
{
    public class CoursesViewModel : BaseViewModel
    {
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public decimal Prices { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Profession { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public int Duration { get; set; }

        public int TeacherID { get; set; }
        public Teacher Teacher { get; set; }
        public ICollection<CourseStudent> CourseStudents { get; set; }
        public bool IsDeleted { get; set; }
        public List<CoursesViewModel> Courses { get; set; } = new List<CoursesViewModel>();
        public StudentViewModel Student { get; set; }
        public ViewType ViewType { get; set; } = ViewType.Default;
    }
}
