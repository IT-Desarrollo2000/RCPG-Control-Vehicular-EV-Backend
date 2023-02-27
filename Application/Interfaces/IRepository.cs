using Domain.Entities;
using System.Linq.Expressions;

namespace Application.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "");
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int Id);
        Task Add(T entity);
        Task Update(T entity);
        Task ToListAsync();
        Task<bool> Delete(int Id);
    }
}
