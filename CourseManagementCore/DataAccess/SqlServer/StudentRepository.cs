using CourseManagementCore.Domain.Abstraction.Interfaces;
using CourseManagementCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementCore.DataAccess.SqlServer
{
    public class StudentRepository : IStudentRepository
    {
        public List<Student> GetStudents()
        {
            throw new NotImplementedException();
        }
    }
}
