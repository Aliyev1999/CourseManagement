namespace CourseManagementCore.Domain.Entities
{
    public class Student : BaseEntity<int>
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime BirthDate { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<CourseStudent> CourseStudents { get; set; }

        //TODO Add profil photo
    }
}