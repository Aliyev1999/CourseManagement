using CourseManagement.Enums;
using CourseManagement.Infrastructure;
using CourseManagement.Mappers;
using CourseManagement.Models;
using CourseManagementEFCore;
using CourseManagementEFCore.Domain.Abstractions;
using CourseManagementEFCore.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagement.Controllers
{
    [Authorize(permission: "", role: "Admin")]
    public class StudentsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppDBContext _context;
        public StudentsController(IUnitOfWork unitOfWork, AppDBContext context)
        {
            this._unitOfWork = unitOfWork;
            this._context = context;
        }

        public async Task<IActionResult> Index(int pg = 1)
        {
            List<StudentViewModel> modelList = new List<StudentViewModel>();
            List<Student> students = await _unitOfWork.StudentRepository.GetAllListIncluding();
            modelList = StudentMapper.GetList(students);
            StudentViewModel model = new StudentViewModel();

            #region for pagination
            Pager pager = new Pager();
            var data = Pager.GetDataPerPage(modelList, pg, ref pager);
            this.ViewBag.Pager = pager;
            #endregion

            model.Students = new List<StudentViewModel>(data);

            return View(model);
        }

        [Authorize(permission: "Create", role: "")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Submit(StudentViewModel model)
        {
            await _context.Students.AddAsync(StudentMapper.DbMapper(model));
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Students");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Student tModel = await _unitOfWork.StudentRepository.GetBy(x => (x.ID == id));
            StudentViewModel model = StudentMapper.ViewMapper(tModel);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Save(StudentViewModel model)
        {
            _context.Students.Update(StudentMapper.DbMapper(model));
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Students");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            Student tModel = await _unitOfWork.StudentRepository.GetBy(x => (x.ID == id));
            StudentViewModel model = StudentMapper.ViewMapper(tModel);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Student tModel = await _unitOfWork.StudentRepository.GetBy(x => (x.ID == id));
            tModel.IsDeleted = true;

            _context.Students.Update(tModel);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Students");
        }
    }
}
