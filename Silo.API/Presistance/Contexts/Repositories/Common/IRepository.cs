using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Silo.API.Presistance.Contexts.Repositories.Common;

public interface IRepository<T> where T : BaseModel, new()
{
    T Add(T entity);
    Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default, bool isHardDelete = false);
    Task DeleteRangeAsync(IEnumerable<int> ids, CancellationToken cancellationToken = default, bool isHardDelete = false);
    IQueryable<T> Get(Expression<Func<T, bool>> predicate);
    Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    void SaveChanges();
    IQueryable<TResult> SelectWhere<TResult>(Expression<Func<T, bool>> predicate, Expression<Func<T, TResult>> selector);
    void Update(T entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(IQueryable<T> query, CancellationToken cancellationToken = default);
    Task<int> UpdateAsync(IQueryable<T> query, Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> setPropertyCalls, CancellationToken cancellationToken = default);
}