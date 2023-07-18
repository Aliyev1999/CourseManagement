using CourseManagementEFCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace CourseManagementEFCore.Domain.Abstractions
{
    public interface IRepository<TEntity, in TPrimaryKey> : IDisposable where TEntity : BaseEntity<TPrimaryKey>
    {
        IQueryable<TEntity> GetAll();

        Task<List<TEntity>> GetAllList();

        Task<TEntity> GetBy(Expression<Func<TEntity, bool>> predicate);

        Task<List<TEntity>> FindBy(Expression<Func<TEntity, bool>> predicate);

        Task<int> Count();

        Task<int> Count(Expression<Func<TEntity, bool>> predicate);

        Task Commit([Optional]CancellationToken cancellationToken);

        IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties);

        Task<List<TEntity>> GetAllListIncluding(params Expression<Func<TEntity, object>>[] includeProperties);

        Task<List<TEntity>> GetAllListIncluding(Expression<Func<TEntity, bool>> includeProperties);

        Task<List<Student>> GetStudentDifferentBy(int courseID);
    }
}
