using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ClinicRepository : IClinicRepository
    {
        private readonly ClinicContext _context;

        public ClinicRepository(ClinicContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Clinic>> GetAllAsync()
        {
            return await _context.Clinics.Include(c => c.City).ToListAsync();
        }

        public async Task<Clinic> GetByIdAsync(int id)
        {
            return await _context.Clinics.Include(c => c.City).FirstOrDefaultAsync(c => c.ClinicId == id);
        }

        public async Task AddAsync(Clinic clinic)
        {
            await _context.Clinics.AddAsync(clinic);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Clinic clinic)
        {
            _context.Clinics.Update(clinic);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var clinic = await _context.Clinics.FindAsync(id);
            if (clinic != null)
            {
                _context.Clinics.Remove(clinic);
                await _context.SaveChangesAsync();
            }
        }
    }
}
