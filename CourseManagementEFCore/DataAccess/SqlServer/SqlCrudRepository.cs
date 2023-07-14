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
    public class SqlCrudRepository<TEntity, TPrimaryKey> : SqlBaseRepository<TEntity, TPrimaryKey>, ICrudRepository<TEntity, TPrimaryKey> where TEntity : BaseEntity<TPrimaryKey>
    {
        private readonly AppDBContext context;
        private DbSet<TEntity> _dbSet;
        public SqlCrudRepository(AppDBContext context) : base(context)
        {
            this.context = context;
            this._dbSet = context.Set<TEntity>();
        }
        public async Task Add(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task Delete(TEntity entity)
        {
            context.Entry(entity).State = EntityState.Deleted;
        }

        public async Task DeleteWhere(Expression<Func<TEntity, bool>> predicate)
        {
            IEnumerable<TEntity> entities = _dbSet.Where(predicate);

            foreach (var entity in entities)
            {
                context.Entry(entity).State = EntityState.Deleted;
            }
        }

        public async Task Update(TEntity entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }
    }
}
