using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class MainService : IMainService
    {
        private readonly ISpecializationRepository _specializationRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly ClinicContext _context;

        public MainService(ISpecializationRepository specializationRepository,
                           IDoctorRepository doctorRepository,
                           ClinicContext context)
        {
            _specializationRepository = specializationRepository;
            _doctorRepository = doctorRepository;
            _context = context;
        }

        public async Task<IEnumerable<SpecializationViewModel>> GetSpecializationsWithDoctorCountsAsync(int? cityId)
        {
            var query = _context.Specializations
                .Select(s => new SpecializationViewModel
                {
                    SpecializationId = s.SpecializationId,
                    Name = s.Name,
                    DoctorCount = s.DoctorSpecializations.Count(ds => !cityId.HasValue || ds.Doctor.DoctorClinics.Any(dc => dc.Clinic.CityId == cityId))
                });

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<DoctorViewModel>> GetDoctorsBySpecializationAsync(int specializationId, int? cityId)
        {
            var query = _context.Doctors
                .Where(d => d.DoctorSpecializations.Any(ds => ds.SpecializationId == specializationId) &&
                            (!cityId.HasValue || d.DoctorClinics.Any(dc => dc.Clinic.CityId == cityId)))
                .Select(d => new DoctorViewModel
                {
                    DoctorId = d.DoctorId,
                    FullName = d.FullName,
                    BirthDate = d.BirthDate,
                    Description = d.Description,
                    Photo = d.Photo,
                    SelectedClinicIds = d.DoctorClinics.Select(dc => dc.ClinicId).ToList(),
                    SelectedSpecializationIds = d.DoctorSpecializations.Select(ds => ds.SpecializationId).ToList(),
                    AvailableClinics = d.DoctorClinics.Select(dc => dc.Clinic).ToList(),
                    AvailableSpecializations = d.DoctorSpecializations.Select(ds => ds.Specialization).ToList()
                });

            return await query.ToListAsync();
        }
    }

}
