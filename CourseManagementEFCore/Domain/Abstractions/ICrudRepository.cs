using CourseManagementEFCore.Domain.Entities;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace CourseManagementEFCore.Domain.Abstractions
{
    public interface ICrudRepository<TEntity, in TPrimaryKey> : IRepository<TEntity, TPrimaryKey> where TEntity : BaseEntity<TPrimaryKey>
    {
        Task Add(TEntity entity);

        Task Update(TEntity entity);

        Task Delete(TEntity entity);

        Task DeleteWhere(Expression<Func<TEntity, bool>> predicate);
    }
}
