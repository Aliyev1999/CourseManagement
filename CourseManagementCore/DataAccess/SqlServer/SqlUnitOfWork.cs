using CourseManagementCore.Domain.Abstraction.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementCore.DataAccess.SqlServer
{
    public class SqlUnitOfWork
    {
        public IStudentRepository EmployeeRepository => new StudentRepository();

    }
}
