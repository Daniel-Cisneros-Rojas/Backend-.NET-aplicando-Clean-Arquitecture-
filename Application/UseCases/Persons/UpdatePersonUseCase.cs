using Application.DTOs.Persons;
using Domain;
using Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Persons
{
    public class UpdatePersonUseCase
    {
        public readonly IRepository <personEntity, Guid> _repository;

        public UpdatePersonUseCase(IRepository<personEntity, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<personEntity> ExecuteAsync(UpdatePersonDto dto) 
        {
            var person = await _repository.GetByIdAsync(dto.Id);
            if (person == null)
            {
                throw new InvalidOperationException($"no existe una persona con el Id {dto.Id}");
            }

            person.UpdatePersonalInfo(dto.FirstName, dto.LastName, dto.Email, dto.PhoneNumber);

            await _repository.UpdateAsync(person);
            await _repository.SaveChangesAsync();
            return person;
        }

    }
}
