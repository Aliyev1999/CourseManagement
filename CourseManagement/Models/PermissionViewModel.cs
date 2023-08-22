using CourseManagementEFCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagement.Models
{
    public class PermissionViewModel
    {
        public bool Delete { get; set; } = false;
        public bool Create { get; set; } = false;
        public bool Update { get; set; } = false;
    }
}
