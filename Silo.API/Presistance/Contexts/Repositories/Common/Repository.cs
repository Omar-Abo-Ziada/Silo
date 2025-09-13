using Microsoft.EntityFrameworkCore.Query;
using Silo.API.Servies.User;
using System.Linq.Expressions;

namespace Silo.API.Presistance.Contexts.Repositories.Common;

public class Repository<T> : IRepository<T> where T : BaseModel, new()
{
    private readonly GeneralDbContext _context;
    private readonly DbSet<T> _dbSet;
    private readonly IUserStateService _userStateService;

    public Repository(GeneralDbContext context, IUserStateService userStateService)
    {
        _userStateService = userStateService;
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FindAsync(new object[] { id }, cancellationToken);
    }

    private IQueryable<T> Get()
    {
        return _dbSet.Where(entity => !entity.IsDeleted);
    }

    public IQueryable<T> Get(Expression<Func<T, bool>> predicate)
    {
        return Get().Where(predicate);
    }

    public IQueryable<TResult> SelectWhere<TResult>(Expression<Func<T, bool>> predicate, Expression<Func<T, TResult>> selector)
    {
        return Get()
            .Where(predicate)
            .Select(selector);
    }


    public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        entity.CreatedBy = _userStateService.UserId;
        entity.CreatedOn = DateTime.UtcNow;
        await _dbSet.AddAsync(entity, cancellationToken);
        return entity;
    }

    public void SaveChanges()
    {
        _context.SaveChanges();
    }

    public T Add(T entity)
    {
        entity.CreatedBy = _userStateService.UserId;
        entity.CreatedOn = DateTime.UtcNow;
        _dbSet.Add(entity);
        return entity;
    }

    public void Update(T entity, CancellationToken cancellationToken = default)
    {
        entity.UpdatedBy = _userStateService.UserId;
        entity.UpdatedOn = DateTime.UtcNow;

        _dbSet.Update(entity);
    }

    public async Task<int> UpdateAsync(
    IQueryable<T> query,
    Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> setPropertyCalls,
    CancellationToken cancellationToken = default)
    {
        return await query.ExecuteUpdateAsync(setPropertyCalls, cancellationToken);
    }


    public async Task<int> UpdateAsync(
    IQueryable<T> query,
    Func<SetPropertyCalls<T>, SetPropertyCalls<T>> setPropertyCalls,
    CancellationToken cancellationToken = default)
    {
        return await query.ExecuteUpdateAsync(updates =>
            setPropertyCalls(
                updates
                    .SetProperty(p => p.UpdatedBy, _userStateService.UserId)
                    .SetProperty(p => p.UpdatedOn, DateTime.UtcNow)
            ),
            cancellationToken);
    }


    public async Task UpdateAsync(IQueryable<T> query, CancellationToken cancellationToken = default)
    {
        var entities = await query.ToListAsync(cancellationToken);

        foreach (var entity in entities)
        {
            entity.UpdatedBy = _userStateService.UserId;
            entity.UpdatedOn = DateTime.UtcNow;

            _dbSet.Update(entity);
        }
    }


    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default, bool isHardDelete = false)
    {
        var entity = await GetByIdAsync(id, cancellationToken);

        if (entity == null)
            return;

        if (isHardDelete)
        {
            // Remove from DbSet so EF issues a DELETE statement
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
        else
        {
            // Soft delete
            entity.delete(); // Assuming this sets IsDeleted = true
            entity.UpdatedOn = DateTime.UtcNow;
            entity.UpdatedBy = _userStateService.UserId;
            Update(entity, cancellationToken);
        }
    }



    public async Task DeleteRangeAsync(IEnumerable<int> ids, CancellationToken cancellationToken = default, bool isHardDelete = false)
    {
        var entities = await _dbSet
            .Where(e => ids.Contains(e.Id) && !e.IsDeleted)
            .ToListAsync(cancellationToken);

        if (!entities.Any())
            return;

        if (isHardDelete)
        {
            _dbSet.RemoveRange(entities);
            //await _context.SaveChangesAsync(cancellationToken);
        }

        else
        {
            foreach (var entity in entities)
            {
                entity.delete();
                entity.UpdatedBy = _userStateService.UserId;
                entity.UpdatedOn = DateTime.UtcNow;
                Update(entity, cancellationToken);
            }
        }

    }


    public Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return Get().AnyAsync(predicate, cancellationToken);
    }
}
