using CourseManagement.Mappers;
using CourseManagement.Models;
using CourseManagementEFCore.Domain.Abstractions;
using CourseManagementEFCore.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagement.Controllers
{
    public class TeachersController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<TeachersController> _logger;
        public TeachersController(IUnitOfWork unitOfWork, ILogger<TeachersController> logger)
        {
            this._unitOfWork = unitOfWork;
            this._logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            List<TeachersViewModel> modelList = new List<TeachersViewModel>();
            List<Teacher> teachers = await _unitOfWork.TeacherRepository.GetAllListIncluding(m => m.Courses);
            modelList = TeacherMappers.GetList(teachers);
            TeachersViewModel model = new TeachersViewModel();

            model.Teachers = modelList;
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Submit(TeachersViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.TeacherRepository.Add(TeacherMappers.DbMapper(model));
                await _unitOfWork.TeacherRepository.Commit();
                return RedirectToAction("Index", "Teachers");
            }
            else
            {
                return View("Create", model);
            }

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            Teacher tModel = await _unitOfWork.TeacherRepository.GetBy(x => (x.ID == id));
            if (tModel == null)
            {
                Response.StatusCode = 404;
                _logger.LogWarning(" Edit : Warning log : " + id.ToString() + " 404 not found");
                return View("NotFound", id.Value);
            }

            TeachersViewModel model = TeacherMappers.ViewMapper(tModel);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Save(TeachersViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.TeacherRepository.Update(TeacherMappers.DbMapper(model));
                await _unitOfWork.TeacherRepository.Commit();
                _logger.LogInformation(" Edit : Information log :" + model.ID.ToString() + " updated");
                return RedirectToAction("Index", "Teachers");
            }
            else
            {
                return View("Edit", model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            Teacher tModel = await _unitOfWork.TeacherRepository.GetBy(x => (x.ID == id));
            TeachersViewModel model = TeacherMappers.ViewMapper(tModel);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Teacher teacher = await _unitOfWork.TeacherRepository.GetBy(x => (x.ID == id));

            var courses = await _unitOfWork.CourseRepository.GetAllListIncluding(x => x.TeacherID == id);
            foreach (var item in courses)
            {
                await _unitOfWork.CourseRepository.Delete(item);
            }

            await _unitOfWork.TeacherRepository.Delete(teacher);
            await _unitOfWork.TeacherRepository.Commit();

            return RedirectToAction("Index", "Teachers");
        }
    }
}
