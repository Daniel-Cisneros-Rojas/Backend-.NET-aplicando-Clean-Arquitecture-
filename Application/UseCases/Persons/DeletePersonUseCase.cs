using Domain;
using Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Persons
{
    public class DeletePersonUseCase
    {
        public readonly IRepository<personEntity, Guid> _repository;

        public DeletePersonUseCase(IRepository<personEntity, Guid> repository)
        {
            _repository = repository;
        }

        public async Task ExecuteAsync(Guid id)
        {
            var person = await _repository.GetByIdAsync(id);
            if (person == null)
            {
                throw new InvalidOperationException($"No se encontro la persona con id {id}");
            }
            await _repository.DeleteAsync(person);
            await _repository.SaveChangesAsync();
             
        }
    }
}
