using Contracts.Common.Interfaces;
using Contracts.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common
{
    public class RepositoryBaseAsync<T, K, TContext> : IRepositoryBaseAsync<T, K, TContext> where T : EntityBase<K> where TContext : DbContext
    {
        private readonly TContext _context;
        private readonly IUnitOfWork<TContext> _unitOfWork;

        public RepositoryBaseAsync(TContext context, IUnitOfWork<TContext> unitOfWork)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public IQueryable<T> FindAll(bool trackChanges = false)
        {
            return !trackChanges ? _context.Set<T>().AsNoTracking() : _context.Set<T>();
        }

        public IQueryable<T> FindAll(bool trackChanges = false, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();

            if (!trackChanges)
            {
                query = query.AsNoTracking();
            }

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return query;
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false)
        {
            return !trackChanges ? _context.Set<T>().Where(expression).AsNoTracking() : _context.Set<T>().Where(expression);
        }

        public async Task<T?> GetByIdAsync(K id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T?> GetByIdAsync(K id, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return await query.FirstOrDefaultAsync(e => e.Id != null && e.Id.Equals(id));
        }

        public async Task<bool> IsExistAsync(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().AnyAsync(expression);
        }

        public async Task<K> CreateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            await _context.Set<T>().AddAsync(entity);
            await SaveChangeAsync();
            return entity.Id;
        }

        public async Task<IEnumerable<K>> CreateListAsync(IEnumerable<T> entities)
        {
            if (entities == null || !entities.Any())
            {
                throw new ArgumentNullException(nameof(entities));
            }

            await _context.Set<T>().AddRangeAsync(entities);
            await SaveChangeAsync();
            return entities.Select(p => p.Id);
        }

        public async Task DeleteAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _context.Set<T>().Remove(entity);
            await SaveChangeAsync();
        }

        public async Task DeleteListAsync(IEnumerable<T> entities)
        {
            if (entities == null || !entities.Any())
            {
                throw new ArgumentNullException(nameof(entities));
            }

            _context.Set<T>().RemoveRange(entities);
            await SaveChangeAsync();
        }

        public async Task<T> UpdateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _context.Set<T>().Update(entity);
            await SaveChangeAsync();
            return entity;
        }

        public async Task<T> UpdateListAsync(IEnumerable<T> entities)
        {
            if (entities == null || !entities.Any())
            {
                throw new ArgumentNullException(nameof(entities));
            }

            _context.Set<T>().UpdateRange(entities);
            await SaveChangeAsync();
            return entities.First();
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }

        public async Task EndTransactionAsync()
        {
            await SaveChangeAsync();
            await CommitTransactionAsync();
        }

        public async Task RollbackTransactionAsync()
        {
            await _context.Database.RollbackTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            await _context.Database.CommitTransactionAsync();
        }

        public async Task<int> SaveChangeAsync()
        {
            return await _unitOfWork.CommitAsync();
        }
    }
}
