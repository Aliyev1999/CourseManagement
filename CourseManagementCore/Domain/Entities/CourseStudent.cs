namespace CourseManagementCore.Domain.Entities
{
    public class CourseStudent : BaseEntity<int>
    {
        public int CourseId { get; set; }

        public int StudentId { get; set; }

        public Course Course { get; set; }

        public Student Student { get; set; }
    }
}

//Course
//Student
//Teachers

//Users
//Permission
//Roles