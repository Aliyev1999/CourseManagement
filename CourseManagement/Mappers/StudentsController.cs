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

            model.Students = new List<StudentViewModel>(modelList);

            return View(model);
        }

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
