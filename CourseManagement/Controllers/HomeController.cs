using CourseManagementCore.Domain.Abstraction.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {

            var a = _unitOfWork.StudentRepository.GetAll();
            return View();
        }
    }
}
