using CourseManagementEFCore.Domain.Abstractions;
using CourseManagementEFCore.Domain.Entities;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CourseManagementEFCore.DataAccess.SqlServer
{
    public class SqlCourseStudentRepository : SqlCrudRepository<CourseStudent, int>, ICourseStudentRepository
    {
        public SqlCourseStudentRepository(AppDBContext context) : base(context)
        {

        }
    }
}
