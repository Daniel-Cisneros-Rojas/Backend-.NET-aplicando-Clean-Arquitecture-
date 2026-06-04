using Domain;
using Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Persons
{
    public class GetPersonByIdUseCase
    {
        private readonly IRepository<personEntity, Guid> _repository;

        public GetPersonByIdUseCase(IRepository<personEntity, Guid> repository)
        {
            _repository = repository;

        }

        public async Task<personEntity> ExecuteAsync(Guid id)
        {
            var person = await _repository.GetByIdAsync(id);

            if (person == null)
            {
                throw new InvalidOperationException($"No se encontro una persona con el Id : {id}");
            }

            return person;
        }
    }
}
