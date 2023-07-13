using CourseManagementCore.DataAccess.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementCore.Domain.Abstraction.Interfaces
{
    public interface IUnitOfWork
    {
        public IStudentRepository StudentRepository { get;}
    }
}
