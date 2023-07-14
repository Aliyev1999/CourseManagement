using CourseManagementEFCore.Domain.Abstractions.Interfaces;
using CourseManagementEFCore.Domain.Entities;

namespace CourseManagementEFCore.Domain.Abstractions
{
    public interface IPermissonRepository : IRepository<Permission, int>
    {

    }
}
