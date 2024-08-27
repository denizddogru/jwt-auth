using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthServer.Core.Repositories;
public interface IGenericRepository<TEntity>where TEntity: class
{
    Task<TEntity> GetByIdAsync(int id);

    Task<IQueryable<TEntity>> GetAllAsync();
}
