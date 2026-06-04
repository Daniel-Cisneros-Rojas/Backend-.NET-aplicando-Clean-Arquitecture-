using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Abstractions
{
    public interface IRepository<TEntity, TId> where TEntity:class
    {
        //Task es para asincrono, <> entre esto lo que se retornara, GetByIdAsync son nombres de las funciones (yo decido como se llaman)
        //TEntity? el ? es para indicar que puede retornar null, GetByIdAsync es para obtener una entidad por su id, recibe un parametro id de tipo TId

        Task<TEntity?> GetByIdAsync(TId id);
        Task<IEnumerable<TEntity>> GetAllAsync();

        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);

        Task DeleteAsync(TEntity entity);

        Task<int> SaveChangesAsync();
    }
}
