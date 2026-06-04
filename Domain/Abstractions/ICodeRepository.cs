using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Abstractions
{
    public interface ICodeRepository<TEntity> where TEntity : class
    {
        //el ? significa que tambien puede retornar null
        Task<TEntity?> GetByCodeAsync(string code);
        Task<bool> ExistWithCodeAsync(string code);

    }
}
