using CourseManagementEFCore.Domain.Entities;

namespace CourseManagementEFCore.Domain.Abstractions
{
    public interface IUserPermissonRepository : ICrudRepository<UserPermission, int>
    {

    }
}
