using CourseManagementEFCore.Domain.Entities;

namespace CourseManagementEFCore.Domain.Abstractions
{
    public interface ICourseStudentRepository : ICrudRepository<CourseStudent, int>
    {

    }
}
