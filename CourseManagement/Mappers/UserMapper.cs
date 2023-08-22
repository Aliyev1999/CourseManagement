using CourseManagement.Enums;
using CourseManagement.Models;
using CourseManagementEFCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagement.Mappers
{
    public static class UserMapper
    {
        public static User DbMapper(UserViewModel viewModel)
        {
            User user = new User();
            user.ID = viewModel.ID;

            user.Email = viewModel.Email;
            user.Name = viewModel.Name;
            user.IsDeleted = viewModel.IsDeleted;
            user.Surname = viewModel.Surname;
            user.PhoneNumber = viewModel.PhoneNumber;
            user.Username = viewModel.Username;
            user.Password = viewModel.Password;
            user.RoleID = viewModel.RoleID;
            user.CreationDate = viewModel.CreationDate;
            user.CreatorID = viewModel.CreatorID;
            user.LastModificatorID = viewModel.LastModificatorID;
            user.LastModificationDate = viewModel.LastModificationDate;
            user.DeletedDate = viewModel.DeletedDate;
            user.DeletedUserID = viewModel.DeletedUserID;
            user.UserPermissions = new List<UserPermission>(viewModel.UserPermissions);

            foreach (var item in viewModel.UserPermissions)
            {
                if (item.Permission.Description == PermissionType.Create.ToString())
                {
                    if (!viewModel.Permissions.Create)
                    {
                        user.UserPermissions.Remove(item);
                    }
                }
                else if (item.Permission.Description == PermissionType.Update.ToString())
                {
                    if (!viewModel.Permissions.Update)
                    {
                        user.UserPermissions.Remove(item);
                    }
                }
                else if (item.Permission.Description == PermissionType.Delete.ToString())
                {
                    if (!viewModel.Permissions.Delete)
                    {
                        user.UserPermissions.Remove(item);
                    }
                }
            }

            return user;
        }

        public static UserViewModel ViewMapper(User user)
        {
            UserViewModel viewModel = new UserViewModel();
            viewModel.ID = user.ID;
            viewModel.IsDeleted = user.IsDeleted;

            if (user.UserPermissions != null)
                viewModel.UserPermissions = new List<UserPermission>(user.UserPermissions);

            viewModel.Email = user.Email;
            viewModel.Name = user.Name;
            viewModel.PhoneNumber = user.PhoneNumber;
            viewModel.Username = user.Username;
            viewModel.Surname = user.Surname;
            viewModel.Password = user.Password;
            viewModel.Role = user.Role;
            viewModel.RoleID = user.RoleID;
            viewModel.UserPermissions = user.UserPermissions;
            viewModel.CreationDate = user.CreationDate;
            viewModel.CreatorID = user.CreatorID;
            viewModel.LastModificatorID = user.LastModificatorID;
            viewModel.LastModificationDate = user.LastModificationDate;
            viewModel.DeletedDate = user.DeletedDate;
            viewModel.DeletedUserID = user.DeletedUserID;

            if (user.UserPermissions != null)
            {
                foreach (var item in user.UserPermissions)
                {
                    if (item.Permission.Description == PermissionType.Create.ToString())
                    {
                        viewModel.Permissions.Create = true;
                    }
                    else if (item.Permission.Description == PermissionType.Update.ToString())
                    {
                        viewModel.Permissions.Update = true;
                    }
                    else if (item.Permission.Description == PermissionType.Delete.ToString())
                    {
                        viewModel.Permissions.Delete = true;
                    }
                }
            }
            return viewModel;
        }

        public static List<UserViewModel> GetList(List<User> users)
        {
            List<UserViewModel> modelList = new List<UserViewModel>();
            UserViewModel model = new UserViewModel();
            foreach (var item in users)
            {
                model = ViewMapper(item);
                modelList.Add(model);
            }
            return modelList;
        }
    }
}
