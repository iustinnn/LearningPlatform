using Microsoft.EntityFrameworkCore;

namespace LearningPlatform.Infrastructure.Implementation;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly DbContext _context;
    private readonly DbSet<TEntity> _entities;

    public Repository(DbContext context)
    {
        _context = context;
        _entities = context.Set<TEntity>();
    }

    public void Add(TEntity entity)
    {
        _entities.Add(entity);
    }

    public void Remove(TEntity entity)
    {
        _entities.Remove(entity);
    }

    public TEntity FindById(object id)
    {
        return _entities.Find(id)!;
    }
}