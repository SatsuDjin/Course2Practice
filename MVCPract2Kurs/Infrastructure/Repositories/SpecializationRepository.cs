using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class SpecializationRepository : ISpecializationRepository
    {
        private readonly ClinicContext _context;

        public SpecializationRepository(ClinicContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Specialization>> GetAllAsync()
        {
            return await _context.Specializations.ToListAsync();
        }

        public async Task<Specialization> GetByIdAsync(int id)
        {
            return await _context.Specializations.FindAsync(id);
        }

        public async Task AddAsync(Specialization specialization)
        {
            await _context.Specializations.AddAsync(specialization);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Specialization specialization)
        {
            _context.Specializations.Update(specialization);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var specialization = await _context.Specializations.FindAsync(id);
            if (specialization != null)
            {
                _context.Specializations.Remove(specialization);
                await _context.SaveChangesAsync();
            }
        }
    }
}
