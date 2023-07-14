using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementEFCore.Domain.Abstractions
{
    public interface IUnitOfWork
    {
        ICourseRepository CourseRepository { get; }
        IStudentRepository StudentRepository { get; }
        ITeacherRepository TeacherRepository { get; }
        IRoleRepository RoleRepository { get; }
        IPermissonRepository PermissonRepository { get; }
        ICourseStudentRepository CourseStudentRepository { get; }
        IUserRepository UserRepository { get; }
        IUserPermissonRepository UserPermissionRepository { get; }
    }
}
