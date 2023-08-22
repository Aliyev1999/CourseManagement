using CourseManagement.Enums;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagement.Infrastructure
{
    public class AuthorizeAttribute : TypeFilterAttribute
    {
        public AuthorizeAttribute(string permission, string role) : base(typeof(AuthorizeActionFilter))
        {
            Arguments = new object[] { permission, role };
        }
    }
}
