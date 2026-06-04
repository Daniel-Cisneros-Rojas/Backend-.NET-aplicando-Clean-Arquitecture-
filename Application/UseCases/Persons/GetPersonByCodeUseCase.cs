using Domain;
using Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Persons
{
    public class GetPersonByCodeUseCase
    {
        private readonly ICodeRepository<personEntity> _codeRepository;

        public GetPersonByCodeUseCase(ICodeRepository<personEntity> codeRepository)
        {
            _codeRepository= codeRepository;
        }

        public async Task<personEntity> ExecuteAsync(string code)
        {
            var person= await _codeRepository.GetByCodeAsync(code);

            if (person == null)
            {
                throw new InvalidOperationException($"No se encontro una persona con el codigo {code}");
            }

            return person;
        }
    }
}
