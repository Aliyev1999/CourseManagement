using CourseManagementCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementCore.Domain.Abstraction.Interfaces
{
    public interface IRepository<T>
    {
        public List<T> GetAll();
        public T Get(int id);
        public int Count();
        public void Delete(int id);
        public void Update(T student);
        public void Add(T student);
        public bool Check(int id);
    }
}
