using Microsoft.AspNetCore.Mvc;

namespace CourseManagement.Infrastructure
{
    public class AllowAnonimousAttribute : TypeFilterAttribute
    {
        public AllowAnonimousAttribute() : base(typeof(AllowAnonimousFilter))
        {

        }
    }
}