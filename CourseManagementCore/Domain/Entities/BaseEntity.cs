using CourseManagementCore.Domain.Abstraction.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementCore.Domain.Entities
{
    public class BaseEntity<T> : IEntity
    {
        public T Id { get; set; }
    }
}
