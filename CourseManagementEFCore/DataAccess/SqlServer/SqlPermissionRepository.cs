using CourseManagementEFCore.Domain.Abstractions;
using CourseManagementEFCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementEFCore.DataAccess.SqlServer
{
    public class SqlPermissionRepository : SqlBaseRepository<Permission, int>, IPermissonRepository
    {
        public SqlPermissionRepository(AppDBContext context) : base(context)
        {

        }
        public Task<List<Permission>> Get()
        {
            throw new NotImplementedException();
        }
    }
}
