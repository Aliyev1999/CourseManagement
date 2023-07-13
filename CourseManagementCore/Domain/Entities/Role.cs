namespace CourseManagementCore.Domain.Entities
{
    public class Role : BaseEntity<int>
    {
        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<User> Users { get; set; }
    }

}