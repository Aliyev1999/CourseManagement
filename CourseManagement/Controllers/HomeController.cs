using CourseManagementEFCore.Domain.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var a = _unitOfWork.TeacherRepository.GetAll().ToList();
            return View();
        }
    }
}
