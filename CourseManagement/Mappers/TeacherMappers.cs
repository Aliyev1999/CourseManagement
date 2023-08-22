using CourseManagement.Models;
using CourseManagementEFCore.Domain.Entities;
using System;
using System.Collections.Generic;

namespace CourseManagement.Mappers
{
    public static class TeacherMappers
    {
        public static Teacher DbMapper(TeachersViewModel viewModel)
        {
            Teacher teacher = new Teacher();
            teacher.ID = viewModel.ID;
            teacher.BirthDate = Convert.ToDateTime(viewModel.BirthDate);

            if (viewModel.Courses != null)
                teacher.Courses = new List<Course>(viewModel.Courses);

            teacher.Email = viewModel.Email;
            teacher.Name = viewModel.Name;
            teacher.IsDeleted = viewModel.IsDeleted;
            teacher.Surname = viewModel.Surname;
            teacher.PhoneNumber = viewModel.PhoneNumber;
            teacher.Profession = viewModel.Profession;
            teacher.CreationDate = viewModel.CreationDate;
            teacher.CreatorID = viewModel.CreatorID;
            teacher.LastModificatorID = viewModel.LastModificatorID;
            teacher.LastModificationDate = viewModel.LastModificationDate;
            teacher.DeletedDate = viewModel.DeletedDate;
            teacher.DeletedUserID = viewModel.DeletedUserID;
            return teacher;
        }

        public static TeachersViewModel ViewMapper(Teacher teacher)
        {
            TeachersViewModel viewModel = new TeachersViewModel();
            viewModel.ID = teacher.ID;
            viewModel.IsDeleted = teacher.IsDeleted;
            viewModel.BirthDate = teacher.BirthDate.ToString("dd/MM/yyyy");

            if (teacher.Courses != null)
                viewModel.Courses = new List<Course>(teacher.Courses);

            viewModel.Email = teacher.Email;
            viewModel.Name = teacher.Name;
            viewModel.PhoneNumber = teacher.PhoneNumber;
            viewModel.Profession = teacher.Profession;
            viewModel.Surname = teacher.Surname;

            viewModel.CreationDate = teacher.CreationDate;
            viewModel.CreatorID = teacher.CreatorID;
            viewModel.LastModificatorID = teacher.LastModificatorID;
            viewModel.LastModificationDate = teacher.LastModificationDate;
            viewModel.DeletedDate = teacher.DeletedDate;
            viewModel.DeletedUserID = teacher.DeletedUserID;
            return viewModel;
        }

        public static List<TeachersViewModel> GetList(List<Teacher> teachers)
        {
            List<TeachersViewModel> modelList = new List<TeachersViewModel>();
            TeachersViewModel model = new TeachersViewModel();
            foreach (var item in teachers)
            {
                model = ViewMapper(item);
                modelList.Add(model);
            }
            return modelList;
        }
    }
}
