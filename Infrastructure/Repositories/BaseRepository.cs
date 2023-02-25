using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    internal class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly CVContext _context;
        private readonly DbSet<T> _entities;

        public BaseRepository(CVContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public async virtual Task<IEnumerable<T>> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<T> query = _entities;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }

        public async Task Add(T entity)
        {
            _entities.Add(entity);
        }

        public async Task Delete(int Id)
        {
            T entity = await GetById(Id);
            _entities.Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _entities.ToListAsync();
        }

        public async Task<T> GetById(int Id)
        {
            return await _entities.FindAsync(Id);
        }

        public async Task Update(T entity)
        {
            entity.UpdatedDate = DateTime.UtcNow;

            _entities.Update(entity);
        }

        Task<bool> IRepository<T>.Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public Task ToListAsync()
        {
            throw new NotImplementedException();
        }
    }
}
