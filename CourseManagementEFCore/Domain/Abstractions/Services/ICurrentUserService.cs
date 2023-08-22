using CourseManagementEFCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagement.Interfaces
{
    public interface ICurrentUserService
    {
        long? UserId { get; set; }
        string Role { get; set; }
        public List<string> Permissions { get; set; }
    }
}
