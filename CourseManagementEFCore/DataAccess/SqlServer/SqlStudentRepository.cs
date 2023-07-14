using CourseManagementEFCore.Domain.Abstractions;
using CourseManagementEFCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementEFCore.DataAccess.SqlServer
{
    public class SqlStudentRepository : SqlCrudRepository<Student, int>, IStudentRepository
    {
        public SqlStudentRepository(AppDBContext context) : base(context)
        {

        }
    }
}
