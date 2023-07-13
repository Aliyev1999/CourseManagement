namespace CourseManagementCore.Domain.Entities
{
    public class Teacher : BaseEntity<int>
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime BirthDate { get; set; }

        public string Profession { get; set; }

        public ICollection<Course> Courses { get; set; }

        public bool IsDeleted { get; set; }

        //TODO Add profil photo
    }
}
