using CourseManagement.Enums;
using CourseManagementEFCore.Domain.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CourseManagement.Models
{
    public class CoursesViewModel : BaseViewModel
    {
<<<<<<< HEAD
        [Required]
        [DataType(DataType.Text)]
=======
>>>>>>> 73d098765aa42eda39f0657a92afaf7acc48f6a8
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public decimal Prices { get; set; }

<<<<<<< HEAD
        [Required]
        [DataType(DataType.Text)]
=======
>>>>>>> 73d098765aa42eda39f0657a92afaf7acc48f6a8
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
