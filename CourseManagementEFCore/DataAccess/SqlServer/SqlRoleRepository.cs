using CourseManagementEFCore.Domain.Abstractions;
using CourseManagementEFCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourseManagementEFCore.DataAccess.SqlServer
{
    public class SqlRoleRepository : SqlBaseRepository<Role,int>, IRoleRepository
    {
        public SqlRoleRepository(AppDBContext context) : base(context)
        {

        }
    }
}
