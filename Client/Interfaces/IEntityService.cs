using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Client.Interfaces;

public interface IEntityService<T>
{
    Task AddAsync(T entity);
    Task<List<T>> GetAllAsync();
}
