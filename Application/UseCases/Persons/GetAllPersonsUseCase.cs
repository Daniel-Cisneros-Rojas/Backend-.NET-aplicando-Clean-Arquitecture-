using Domain;
using Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Persons
{
    public class GetAllPersonsUseCase
    {
        //el guion bajo indica que es algo privado
        private readonly IRepository<personEntity, Guid> _repository;

        public GetAllPersonsUseCase(IRepository<personEntity, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<personEntity>> ExecuteAsync()
        {
            return await _repository.GetAllAsync();

        }
    }
}
