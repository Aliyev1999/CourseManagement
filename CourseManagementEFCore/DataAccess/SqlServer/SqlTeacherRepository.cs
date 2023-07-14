using CourseManagementEFCore.Domain.Abstractions;
using CourseManagementEFCore.Domain.Entities;

namespace CourseManagementEFCore.DataAccess.SqlServer
{
    public class SqlTeacherRepository : SqlCrudRepository<Teacher, int>, ITeacherRepository
    {
        public SqlTeacherRepository(AppDBContext context) : base(context)
        {

        }
    }
}
