using CourseManagement.Models;
using CourseManagementEFCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagement.Mappers
{
    public class CourseMapper
    {
        public static Course DbMapper(CoursesViewModel viewModel)
        {
            Course course = new Course();
            course.ID = viewModel.ID;
            course.Prices = viewModel.Prices;

            if (viewModel.CourseStudents != null)
                course.CourseStudents = new List<CourseStudent>(viewModel.CourseStudents);

            course.Description = viewModel.Description;
            course.Name = viewModel.Name;
            course.IsDeleted = viewModel.IsDeleted;
            course.Duration = viewModel.Duration;
            course.Profession = viewModel.Profession;
            course.Teacher = viewModel.Teacher;
            course.TeacherID = viewModel.TeacherID;
            course.CreationDate = viewModel.CreationDate;
            course.CreatorID = viewModel.CreatorID;
            course.LastModificatorID = viewModel.LastModificatorID;
            course.LastModificationDate = viewModel.LastModificationDate;
            course.DeletedDate = viewModel.DeletedDate;
            course.DeletedUserID = viewModel.DeletedUserID;
            return course;
        }

        public static CoursesViewModel ViewMapper(Course course)
        {
            CoursesViewModel viewModel = new CoursesViewModel();
            viewModel.ID = course.ID;
            viewModel.IsDeleted = course.IsDeleted;
            viewModel.Duration = course.Duration;

            if (course.CourseStudents != null)
                viewModel.CourseStudents = new List<CourseStudent>(course.CourseStudents);

            viewModel.Description = course.Description;
            viewModel.Name = course.Name;
            viewModel.Teacher = course.Teacher;
            viewModel.Profession = course.Profession;
            viewModel.TeacherID = course.TeacherID;
            viewModel.Prices = course.Prices;
            viewModel.CreationDate = course.CreationDate;
            viewModel.CreatorID = course.CreatorID;
            viewModel.LastModificatorID = course.LastModificatorID;
            viewModel.LastModificationDate = course.LastModificationDate;
            viewModel.DeletedDate = course.DeletedDate;
            viewModel.DeletedUserID = course.DeletedUserID;

            return viewModel;
        }

        public static List<CoursesViewModel> GetList(List<Course> courses)
        {
            List<CoursesViewModel> modelList = new List<CoursesViewModel>();
            CoursesViewModel model = new CoursesViewModel();
            foreach (var item in courses)
            {
                model = ViewMapper(item);
                modelList.Add(model);
            }
            return modelList;
        }
    }
}
