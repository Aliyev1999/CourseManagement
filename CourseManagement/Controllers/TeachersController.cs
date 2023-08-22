using CourseManagement.Enums;
using CourseManagement.Infrastructure;
using CourseManagement.Mappers;
using CourseManagement.Models;
using CourseManagementEFCore;
using CourseManagementEFCore.Domain.Abstractions;
using CourseManagementEFCore.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.Controllers
{

    [Authorize(permission: "", role: "Admin")]
    public class TeachersController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppDBContext _context;
        private readonly ILogger<TeachersController> _logger;
        public TeachersController(IUnitOfWork unitOfWork, AppDBContext context, ILogger<TeachersController> logger)
        {
            this._unitOfWork = unitOfWork;
            this._context = context;
            this._logger = logger;
        }

        public async Task<IActionResult> Index(int pg = 1)
        {
            List<TeachersViewModel> modelList = new List<TeachersViewModel>();
            List<Teacher> teachers = await _unitOfWork.TeacherRepository.GetAllListIncluding(m => m.Courses);
            modelList = TeacherMappers.GetList(teachers);
            TeachersViewModel model = new TeachersViewModel();

            #region for pagination
            Pager pager = new Pager();
            var data = Pager.GetDataPerPage(modelList, pg, ref pager);
            this.ViewBag.Pager = pager;
            #endregion
            model.Teachers = new List<TeachersViewModel>(data);
            model.IsFromCourse = false;
            return View(model);
        }

        [Authorize(permission: "Create", role: "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Submit(TeachersViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _context.Teachers.AddAsync(TeacherMappers.DbMapper(model));
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Teachers");
            }
            else
            {
                return View("Create", model);
            }

        }

        [Authorize(permission: "Update", role: "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            _logger.LogTrace(" Edit : Trace log");
            _logger.LogDebug(" Edit : Debug log");

            _logger.LogError(" Edit : Error log");
            _logger.LogCritical(" Edit : Critical log");

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
                _context.Teachers.Update(TeacherMappers.DbMapper(model));
                await _context.SaveChangesAsync();
                _logger.LogInformation(" Edit : Information log :" + model.ID.ToString() + " updated");
                return RedirectToAction("Index", "Teachers");
            }
            else
            {
                return View("Edit", model);
            }
        }

        [AllowAnonimous]
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            _logger.LogTrace(" Details : Trace log : " + id.ToString() + " details");
            Teacher tModel = await _unitOfWork.TeacherRepository.GetBy(x => (x.ID == id));
            TeachersViewModel model = TeacherMappers.ViewMapper(tModel);
            return View(model);
        }

        [Authorize(permission: "Delete", role: "Admin")]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Teacher tModel = await _unitOfWork.TeacherRepository.GetBy(x => (x.ID == id));
            tModel.IsDeleted = true;

            var courses = await _unitOfWork.CourseRepository.GetAllListIncluding(x => x.TeacherID == id);
            foreach (var item in courses)
            {
                item.IsDeleted = true;
                _context.Courses.Update(item);
            }

            _context.Teachers.Update(tModel);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Teachers");
        }
    }
}
