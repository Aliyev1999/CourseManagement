using CourseManagementEFCore.Domain.Entities;

namespace CourseManagementEFCore.Domain.Abstractions
{
    public interface ICourseRepository : ICrudRepository<Course, int>
    {

    }
}
