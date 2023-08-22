using CourseManagement.Models;
using CourseManagementEFCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagement.Mappers
{
    public class StudentMapper
    {
        public static Student DbMapper(StudentViewModel viewModel)
        {
            Student student = new Student();
            student.ID = viewModel.ID;
            student.BirthDate = viewModel.BirthDate;

            if (viewModel.CourseStudents != null)
                student.CourseStudents = new List<CourseStudent>(viewModel.CourseStudents);

            student.Surname = viewModel.Surname;
            student.Name = viewModel.Name;
            student.IsDeleted = viewModel.IsDeleted;
            student.PhoneNumber = viewModel.PhoneNumber;
            student.Email = viewModel.Email;
            student.CreationDate = viewModel.CreationDate;
            student.CreatorID = viewModel.CreatorID;
            student.LastModificatorID = viewModel.LastModificatorID;
            student.LastModificationDate = viewModel.LastModificationDate;
            student.DeletedDate = viewModel.DeletedDate;
            student.DeletedUserID = viewModel.DeletedUserID;
            return student;
        }

        public static StudentViewModel ViewMapper(Student student)
        {
            StudentViewModel viewModel = new StudentViewModel();
            viewModel.ID = student.ID;
            viewModel.IsDeleted = student.IsDeleted;
            viewModel.Surname = student.Surname;

            if (student.CourseStudents != null)
                viewModel.CourseStudents = new List<CourseStudent>(student.CourseStudents);

            viewModel.BirthDate = student.BirthDate;
            viewModel.Name = student.Name;
            viewModel.PhoneNumber = student.PhoneNumber;
            viewModel.Email = student.Email;
            viewModel.CreationDate = student.CreationDate;
            viewModel.CreatorID = student.CreatorID;
            viewModel.LastModificatorID = student.LastModificatorID;
            viewModel.LastModificationDate = student.LastModificationDate;
            viewModel.DeletedDate = student.DeletedDate;
            viewModel.DeletedUserID = student.DeletedUserID;
            return viewModel;
        }

        public static List<StudentViewModel> GetList(List<Student> courses)
        {
            List<StudentViewModel> modelList = new List<StudentViewModel>();

            StudentViewModel model = new StudentViewModel();
            foreach (var item in courses)
            {
                model = ViewMapper(item);
                modelList.Add(model);
            }
            return modelList;
        }
    }
}
