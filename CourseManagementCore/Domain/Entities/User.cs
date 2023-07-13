namespace CourseManagementCore.Domain.Entities
{
    public class User : BaseEntity<int>
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public DateTime BirthDate { get; set; }

        public int RoleId { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<Permission> Permissions { get; set; }

        public Role Role { get; set; }
        //TODO add meetin times
    }


}
