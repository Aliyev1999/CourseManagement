using CourseManagementEFCore.Domain.Entities;

namespace CourseManagementEFCore.Domain.Abstractions
{
    public interface IUserRepository : ICrudRepository<User, int>
    {

    }
}
