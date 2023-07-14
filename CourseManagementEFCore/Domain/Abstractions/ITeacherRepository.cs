using CourseManagementEFCore.Domain.Abstractions.Interfaces;
using CourseManagementEFCore.Domain.Entities;

namespace CourseManagementEFCore.Domain.Abstractions
{
    public interface ITeacherRepository : ICrudRepository<Teacher, int>
    {

    }
}
