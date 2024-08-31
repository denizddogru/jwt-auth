using AuthServer.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AuthServer.Core.Repositories;

// Generic TEntity , generic repository pattern'ini kullanır. Herhangi bir entity tipi ile çalışmasını sağlar
// Sağladığı avantajlar ise User, Products, orders gibi farklı entitiler için kod tekrarından kurtarır.
public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{
    private readonly DbContext _context;
    private readonly DbSet<TEntity> _dbSet;

    public GenericRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }
    public async Task<TEntity> GetByIdAsync(int id)
    {
        var entity = await _dbSet.FindAsync(id);

        if(entity!=null)
        {
            // memory'de track edilmesin. Servis classında daha detaylandırılacak.
            // TODO
            _context.Entry(entity).State = EntityState.Detached;
        }

        return entity;
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
       return await _dbSet.ToListAsync();
    }

    public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
    {
        return _dbSet.Where(predicate);
    }

    public async Task AddAsync(TEntity entity)
    {
        // memory'ye bir tane entity ekledik, daha db'ye kaydetmedik
        
        await _dbSet.AddAsync(entity);

        // unitOfWork ile kaydedince DB'ye yansıyacak.
    }

    public void Remove(TEntity entity)
    {
        _dbSet.Remove(entity);

        //memoryden entity'yi sildik ama henüz save changes async yapmadığımız için db'ye yansımadı.
    }

    public TEntity Update(TEntity entity)
    {
        _context.Entry(entity).State = EntityState.Modified;

        return entity;
    }
}
