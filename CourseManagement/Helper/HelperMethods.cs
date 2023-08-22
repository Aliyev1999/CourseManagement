using CourseManagementEFCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagement.Helper
{
    public static class HelperMethods
    {
        public static List<string> GetPermissionStringList(List<UserPermission> userPermissions)
        {
            List<string> permissions = new List<string>();
            foreach (var item in userPermissions)
            {
                permissions.Add(item.Permission.Description);
            }
            return permissions;
        }

    }
}
