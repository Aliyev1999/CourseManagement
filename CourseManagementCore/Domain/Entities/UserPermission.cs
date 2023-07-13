namespace CourseManagementCore.Domain.Entities
{
    public class UserPermission : BaseEntity<int>
    {
        public int UserId { get; set; }

        public int PermissionId { get; set; }

        public User User { get; set; }

        public Permission Permission { get; set; }
    }

}
