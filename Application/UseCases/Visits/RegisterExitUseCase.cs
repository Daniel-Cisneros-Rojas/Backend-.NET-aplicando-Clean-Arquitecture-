using Application.DTOs.Visit;
using Domain;
using Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Visits
{
    public class RegisterExitUseCase
    {
        private readonly IRepository<VisitEntity, Guid> _repository;
        private readonly IVisitRepository<VisitEntity> _visitRepository;

        public RegisterExitUseCase(IRepository<VisitEntity, Guid> repository, IVisitRepository<VisitEntity> visitRepository)
        {
            _repository = repository;
            _visitRepository = visitRepository;
        }


        public async Task<VisitEntity> ExecuteAsync(RegisterExitDto dto)
        {
            VisitEntity? visit;

            if (dto.VisitId.HasValue)
            {
                visit = await _repository.GetByIdAsync(dto.VisitId.Value);

                if (visit == null)
                {
                    throw new InvalidOperationException($"No se encontro una visita con el ID {dto.VisitId}");
                }
            }
            else if (!string.IsNullOrWhiteSpace(dto.Code))
            {
                visit = await _visitRepository.GetActiveVisitByPersonCodeAsync(dto.Code);

                if (visit == null)
                {
                    throw new InvalidOperationException($"No se encontro una visita con el codigo {dto.Code}");
                }
            }
            else
            {
                throw new InvalidOperationException("Debe proporcionar visitID o codigo para registrar la salida");
            }
        }
    }
}
