using Data.Persistence;
using Domain;
using Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    public class PersonRepository : IRepository<personEntity, Guid>, ICodeRepository<personEntity>
    {
        private readonly ApplicationDbContext _context;

        public PersonRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        //aqui se hace el como se va a hacer, esta primera funcion como se va a realizar en GetByIdAsync establecido en IRePOSITORY
        public async Task<personEntity?> GetByIdAsync(Guid id)
        {
            return await _context.Persons.FirstOrDefaultAsync(p => p.Id == id);

        }

        public async Task<IEnumerable<personEntity>> GetAllAsync()
        {
            //Estas serian como las consultas sql
            return await _context.Persons
                .AsNoTracking() //desabilita rastreo para que sea mas rapido
                .OrderBy(p => p.FirstName)
                .ThenBy(p => p.LastName)
                .ToListAsync();
        }

        public async Task AddAsync(personEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            await _context.Persons.AddAsync(entity);
        }

        public Task UpdateAsync(personEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _context.Persons.Update(entity);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(personEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            _context.Persons.Remove(entity);
            return Task.CompletedTask;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        //IcodeRepository 
        public async Task<personEntity?> GetByCodeAsync(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                throw new ArgumentException("el codigo no puede estar vasio", nameof(code));
            }
            var normalizedCode = code.ToUpperInvariant();

            return await _context.Persons.FirstOrDefaultAsync(p => p.Code == normalizedCode);
        }

        public async Task<bool> ExistWithCodeAsync(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                throw new ArgumentException($"{code} El codigo no puede estar vazio", nameof(code));
            }
            var normalizedCode = code.ToUpperInvariant();
            return await _context.Persons.AnyAsync(p => p.Code == normalizedCode);// AnyAsync devuelve true si encuentra al menos un registro que cumpla la condicion, de lo contrario devuelve false
        }
    }
}
