using CourseManagementEFCore.Domain.Abstractions;
using CourseManagementEFCore.Domain.Entities;

namespace CourseManagementEFCore.DataAccess.SqlServer
{
    public class SqlCourseRepository : SqlCrudRepository<Course, int>,  ICourseRepository
    {
        public SqlCourseRepository(AppDBContext context) : base(context)
        {

        }
    }
}
