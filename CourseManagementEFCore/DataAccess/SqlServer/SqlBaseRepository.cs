using CourseManagementEFCore.Domain.Abstractions;
using CourseManagementEFCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CourseManagementEFCore.DataAccess.SqlServer
{
    public class SqlBaseRepository<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey> where TEntity : BaseEntity<TPrimaryKey>
    {
        private readonly AppDBContext context;
        private DbSet<TEntity> _dbSet;
        public SqlBaseRepository(AppDBContext context)
        {
            this.context = context;
            this._dbSet = context.Set<TEntity>();
        }

        public async Task<int> Count()
        {
            return await _dbSet.CountAsync();
        }

        public Task<int> Count(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.CountAsync(predicate);
        }

        public void Dispose()
        {
            context?.Dispose();
        }

        public ValueTask<TEntity> Find(TPrimaryKey id)
        {
            return _dbSet.FindAsync(id);
        }

        public async Task<List<TEntity>> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetAll().Where(predicate).ToListAsync();
        }

        public IQueryable<TEntity> GetAll()
        {
            IQueryable<TEntity> table = _dbSet;
            return table;
        }

        public async Task<List<TEntity>> GetAllList()
        {
            return await GetAll().ToListAsync();
        }

        public IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = GetAll();
            includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            return query;
        }

        public Task<List<TEntity>> GetAllListIncluding(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = GetAll();
            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            return query.ToListAsync();
        }

        public Task<List<TEntity>> GetAllListIncluding(Expression<Func<TEntity, bool>> includeProperties)
        {
            var query = GetAll();
            query = query.Where(includeProperties);
            return query.ToListAsync();
        }

        public async Task Commit(CancellationToken cancellationToken = new CancellationToken())
        {
            await context.SaveChangesAsync(cancellationToken);
        }

        public Task<TEntity> GetBy(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().SingleOrDefaultAsync(predicate);
        }

        private static void BindIncludeProperties(IQueryable<TEntity> query, IEnumerable<Expression<Func<TEntity, object>>> includeProperties)
        {
            includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

        public async Task<List<Student>> GetStudentDifferentBy(int courseID)
        {
            var cStudent = context.CourseStudents.Where(x => x.CourseID == courseID).Select(x => x.StudentID);
            return context.Students.Where(x => !cStudent.Contains(x.ID)).ToList();
        }
    }
}
