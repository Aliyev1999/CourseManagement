namespace CourseManagementCore.Domain.Entities
{
    public class Permission: BaseEntity<int>
    {
        public string Description { get; set; }

        public bool IsOkey { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<User> Users { get; set; }

    }


}




//Course
//Student
//Teachers

//Users
//Permission
//Roles