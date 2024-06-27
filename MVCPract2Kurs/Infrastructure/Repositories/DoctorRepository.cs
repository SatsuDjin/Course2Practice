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
    public class DoctorRepository : IDoctorRepository
    {
        private readonly ClinicContext _context;

        public DoctorRepository(ClinicContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Doctor>> GetAllAsync()
        {
            return await _context.Doctors.Include(d => d.DoctorClinics).ThenInclude(dc => dc.Clinic)
                                         .Include(d => d.DoctorSpecializations).ThenInclude(ds => ds.Specialization)
                                         .ToListAsync();
        }

        public async Task<Doctor> GetByIdAsync(int id)
        {
            return await _context.Doctors.Include(d => d.DoctorClinics).ThenInclude(dc => dc.Clinic)
                                         .Include(d => d.DoctorSpecializations).ThenInclude(ds => ds.Specialization)
                                         .FirstOrDefaultAsync(d => d.DoctorId == id);
        }

        public async Task AddAsync(Doctor doctor)
        {
            await _context.Doctors.AddAsync(doctor);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Doctor doctor)
        {
            _context.Doctors.Update(doctor);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor != null)
            {
                _context.Doctors.Remove(doctor);
                await _context.SaveChangesAsync();
            }
        }
    }
}
