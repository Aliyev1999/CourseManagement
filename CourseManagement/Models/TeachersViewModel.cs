using CourseManagementEFCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagement.Models
{
    public class TeachersViewModel : BaseViewModel
    {
        [Required(ErrorMessage ="Name is required")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Surname { get; set; }

        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string BirthDate { get; set; }
        public string Profession { get; set; }
        public ICollection<Course> Courses { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<TeachersViewModel> Teachers { get; set; }
        public bool IsFromCourse { get; set; }
    }
}
