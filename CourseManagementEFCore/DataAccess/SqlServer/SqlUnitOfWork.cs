using CourseManagementEFCore.Domain;
using CourseManagementEFCore.Domain.Abstractions;

namespace CourseManagementEFCore.DataAccess.SqlServer
{
    public class SqlUnitOfWork : IUnitOfWork
    {
        private readonly AppDBContext _sqlContext;

        public SqlUnitOfWork(AppDBContext db)
        {
            _sqlContext = db;

        }

        public ICourseRepository CourseRepository => new SqlCourseRepository(_sqlContext);

        public IStudentRepository StudentRepository => new SqlStudentRepository(_sqlContext);

        public ITeacherRepository TeacherRepository => new SqlTeacherRepository(_sqlContext);

        public IRoleRepository RoleRepository => new SqlRoleRepository(_sqlContext);

        public IPermissonRepository PermissonRepository => new SqlPermissionRepository(_sqlContext);

        public ICourseStudentRepository CourseStudentRepository => new SqlCourseStudentRepository(_sqlContext);

        public IUserRepository UserRepository => new SqlUserRepository(_sqlContext);

        public IUserPermissonRepository UserPermissionRepository => new SqlUserPermissionRepository(_sqlContext);

    }
}
