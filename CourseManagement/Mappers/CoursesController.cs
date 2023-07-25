using CourseManagement.Enums;
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
    public class CoursesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppDBContext _context;
        public CoursesController(IUnitOfWork unitOfWork, AppDBContext context)
        {
            this._unitOfWork = unitOfWork;
            this._context = context;
        }

        public async Task<IActionResult> Index(int pg = 1)
        {
            List<CoursesViewModel> modelList = new List<CoursesViewModel>();
            List<Course> teachers = await _unitOfWork.CourseRepository.GetAllListIncluding();
            modelList = CourseMapper.GetList(teachers);
            CoursesViewModel model = new CoursesViewModel();

            model.Courses = new List<CoursesViewModel>(modelList);

            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            var students = await _unitOfWork.StudentRepository.GetAllList();
            var teacherList = await _unitOfWork.TeacherRepository.GetAllList();
            List<ModelForView> modelTeachers = new List<ModelForView>();
            foreach (var item in teacherList)
            {
                ModelForView model = new ModelForView();
                model.Details = item.Name + " " + item.Surname + " - " + item.Profession;
                model.ID = item.ID;
                modelTeachers.Add(model);
            }
            List<ModelForView> modelStudents = new List<ModelForView>();
            foreach (var item in students)
            {
                ModelForView model = new ModelForView();
                model.Details = item.Name + " " + item.Surname;
                model.ID = item.ID;
                modelStudents.Add(model);
            }

            CoursesViewModel co = new CoursesViewModel();
            co.Student = new StudentViewModel();
            co.Student.Students = new List<StudentViewModel>();
            foreach (var item in students)
            {
                StudentViewModel vm = new StudentViewModel();
                vm = StudentMapper.ViewMapper(item);

                co.Student.Students.Add(vm);
            }

            this.ViewBag.TeacherList = modelTeachers;
            this.ViewBag.StudentsList = modelStudents;
            return View(co);
        }

        [HttpPost]
        public async Task<IActionResult> Submit(CoursesViewModel model)
        {
            await _context.Courses.AddAsync(CourseMapper.DbMapper(model));
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Courses");


        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Course tModel = await _unitOfWork.CourseRepository.GetBy(x => (x.ID == id));
            Teacher teacher = await _unitOfWork.TeacherRepository.GetBy(x => (x.ID == tModel.TeacherID));

            var courseStudent = await _unitOfWork.CourseStudentRepository.GetAllListIncluding(x => x.CourseID == id);
            var ct = new List<StudentViewModel>();
            foreach (var item in courseStudent)
            {
                var st = await _unitOfWork.StudentRepository.GetBy(x => x.ID == item.StudentID);
                var stVM = StudentMapper.ViewMapper(st);
                ct.Add(stVM);
            }

            var students = await _unitOfWork.StudentRepository.GetAllList();
            var teacherList = await _unitOfWork.TeacherRepository.GetAllList();

            List<ModelForView> modelTeachers = new List<ModelForView>();
            foreach (var item in teacherList)
            {
                ModelForView models = new ModelForView();
                models.Details = item.Name + " " + item.Surname + " - " + item.Profession;
                models.ID = item.ID;
                modelTeachers.Add(models);
            }
            List<ModelForView> modelStudents = new List<ModelForView>();
            foreach (var item in students)
            {
                ModelForView models = new ModelForView();
                models.Details = item.Name + " " + item.Surname;
                models.ID = item.ID;
                modelStudents.Add(models);
            }
            tModel.Teacher = teacher;

            this.ViewBag.TeacherList = modelTeachers;
            this.ViewBag.StudentsList = modelStudents;
            this.ViewBag.CourseStudent = ct;
            CoursesViewModel model = CourseMapper.ViewMapper(tModel);
            model.Student = new StudentViewModel();
            model.Student.Students = new List<StudentViewModel>(ct);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Save(CoursesViewModel model)
        {
            _context.Courses.Update(CourseMapper.DbMapper(model));
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Courses");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            Course tModel = await _unitOfWork.CourseRepository.GetBy(x => (x.ID == id));
            Teacher teacher = await _unitOfWork.TeacherRepository.GetBy(x => (x.ID == tModel.TeacherID));
            tModel.Teacher = teacher;
            CoursesViewModel model = CourseMapper.ViewMapper(tModel);
            return View(model);
        }


        public async Task<IActionResult> AddStudents(CoursesViewModel model)
        {
            var students = await _unitOfWork.StudentRepository.GetAllList();
            var stVMList = new List<StudentViewModel>();
            foreach (var item in students)
            {
                StudentViewModel vm = StudentMapper.ViewMapper(item);
                stVMList.Add(vm);
            }
            model.Student = new StudentViewModel();
            model.Student.Students = new List<StudentViewModel>(stVMList);
            return View(model);
        }


        public async Task<IActionResult> AddTeacher(TeachersViewModel model)
        {

            await _context.SaveChangesAsync();
            return RedirectToAction("Create", "Courses");
        }


        public async Task<IActionResult> NotExisting(int courseID)
        {
            var stCoVM = await _unitOfWork.CourseStudentRepository.GetStudentDifferentBy(courseID);
            var cModel = new CoursesViewModel();
            var stVMList = new List<StudentViewModel>();
            cModel.Student = new StudentViewModel();
            foreach (var item in stCoVM)
            {
                StudentViewModel vm = StudentMapper.ViewMapper(item);
                stVMList.Add(vm);
            }
            cModel.Student.Students = stVMList;
            cModel.ViewType = ViewType.NoExisting;
            cModel.ID = courseID;
            return View("AddStudents", cModel);
        }


        public async Task<IActionResult> Existing(int courseID)
        {
            var stCoVM = await _unitOfWork.CourseStudentRepository.GetAllListIncluding(x => x.CourseID == courseID);
            var cModel = new CoursesViewModel();
            var stVMList = new List<StudentViewModel>();
            cModel.Student = new StudentViewModel();
            foreach (var item in stCoVM)
            {
                var st = await _unitOfWork.StudentRepository.GetBy(x => x.ID == item.StudentID);
                StudentViewModel vm = StudentMapper.ViewMapper(st);
                stVMList.Add(vm);
            }
            cModel.Student.Students = stVMList;
            cModel.ViewType = ViewType.Existing;
            cModel.ID = courseID;
            return View("AddStudents", cModel);
        }


        public async Task<IActionResult> AddStudentToCourse(int studentID, int courseID)
        {
            var course = await _unitOfWork.CourseRepository.GetBy(x => x.ID == courseID);
            CoursesViewModel vm = CourseMapper.ViewMapper(course);
            var courseStu = new CourseStudent();
            courseStu.StudentID = studentID;
            courseStu.CourseID = courseID;
            await _context.AddAsync(courseStu);
            await _context.SaveChangesAsync();
            vm.ViewType = ViewType.Default;
            return RedirectToAction("AddStudents", "Courses", vm);
        }

        public async Task<IActionResult> RemoveStudentFromCourse(int studentID, int courseID)
        {
            var course = await _unitOfWork.CourseRepository.GetBy(x => x.ID == courseID);
            CoursesViewModel vm = CourseMapper.ViewMapper(course);
            var courseStu = await _unitOfWork.CourseStudentRepository.GetBy(x => (x.CourseID == courseID) && (x.StudentID == studentID));
            _context.CourseStudents.Remove(courseStu);
            await _context.SaveChangesAsync();
            vm.ViewType = ViewType.Default;
            return RedirectToAction("AddStudents", "Courses", vm);
        }
    }
}
