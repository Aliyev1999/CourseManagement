using CourseManagementCore.Domain.Abstraction.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementCore.DataAccess.SqlServer
{
    public class SqlUnitOfWork : IUnitOfWork
    {
        string _connectionString;
        public SqlUnitOfWork(string connectionString)
        {
            _connectionString = connectionString;
        }
        
        public IStudentRepository StudentRepository => new SqlStudentRepository(_connectionString);

    }
}
