using CourseManagementEFCore.Domain.Abstractions.Interfaces;

namespace CourseManagementEFCore.Domain.Entities
{
    public abstract class BaseEntity<T> : IEntity
    {
        public T ID { get; set; }
    }
}
