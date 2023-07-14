using CourseManagementEFCore.Domain.Abstractions;
using CourseManagementEFCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementEFCore.DataAccess.SqlServer
{
    public class SqlUserPermissionRepository : SqlCrudRepository<UserPermission, int>, IUserPermissonRepository
    {
        public SqlUserPermissionRepository(AppDBContext context) : base(context)
        {

        }
    }
}
