using Application.DTOs.Persons;
using Domain;
using Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Application.UseCases.Persons
{
    public class CreatePersonUseCase
    {
        private readonly IRepository <personEntity, Guid> _repository;
        private readonly ICodeRepository <personEntity> _codeRepository;

        public CreatePersonUseCase(IRepository<personEntity, Guid> repository, ICodeRepository <personEntity> codeRepository)
        {
            _repository = repository;
            _codeRepository = codeRepository;
        }
        
        public async Task<personEntity> ExecuteAsync(CreatePersonDto dto)
        {
            if (await _codeRepository.ExistWithCodeAsync(dto.Code))
            {
                throw new InvalidOperationException("El codigo ya eciste en el sistema");
            }
            var person = new personEntity(
                dto.Code, dto.FirstName, dto.LastName, dto.Email, dto.PhoneNumber);
            await _repository.AddAsync(person);
            await _repository.SaveChangesAsync();
            return person;
        }


    }
}
