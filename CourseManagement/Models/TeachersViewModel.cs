﻿using CourseManagementEFCore.Domain.Entities;
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
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string BirthDate { get; set; }
        public string Profession { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<Course> Courses { get; set; } = new List<Course>();
        public ICollection<TeachersViewModel> Teachers { get; set; }= new List<TeachersViewModel>();
    }
}
