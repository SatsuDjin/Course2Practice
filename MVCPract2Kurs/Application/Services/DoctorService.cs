using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IClinicRepository _clinicRepository;
        private readonly ISpecializationRepository _specializationRepository;

        public DoctorService(IDoctorRepository doctorRepository, IClinicRepository clinicRepository, ISpecializationRepository specializationRepository)
        {
            _doctorRepository = doctorRepository;
            _clinicRepository = clinicRepository;
            _specializationRepository = specializationRepository;
        }

        public async Task<IEnumerable<Doctor>> GetAllDoctorsAsync()
        {
            return await _doctorRepository.GetAllAsync();
        }

        public async Task<Doctor> GetDoctorByIdAsync(int id)
        {
            return await _doctorRepository.GetByIdAsync(id);
        }

        public async Task AddDoctorAsync(DoctorViewModel model)
        {
            var doctor = new Doctor
            {
                FullName = model.FullName,
                BirthDate = DateTime.SpecifyKind(model.BirthDate, DateTimeKind.Utc),
                Description = model.Description,
                Photo = model.Photo,
                DoctorClinics = model.SelectedClinicIds.Select(id => new DoctorClinic { ClinicId = id }).ToList(),
                DoctorSpecializations = model.SelectedSpecializationIds.Select(id => new DoctorSpecialization { SpecializationId = id }).ToList()
            };

            await _doctorRepository.AddAsync(doctor);
        }

        public async Task UpdateDoctorAsync(DoctorViewModel model)
        {
            var doctor = await _doctorRepository.GetByIdAsync(model.DoctorId);
            if (doctor != null)
            {
                doctor.FullName = model.FullName;
                doctor.BirthDate = DateTime.SpecifyKind(model.BirthDate, DateTimeKind.Utc);
                doctor.Description = model.Description;
                doctor.Photo = model.Photo;

                
                doctor.DoctorClinics.Clear();
                doctor.DoctorSpecializations.Clear();

                
                foreach (var clinicId in model.SelectedClinicIds)
                {
                    doctor.DoctorClinics.Add(new DoctorClinic { ClinicId = clinicId });
                }

                foreach (var specId in model.SelectedSpecializationIds)
                {
                    doctor.DoctorSpecializations.Add(new DoctorSpecialization { SpecializationId = specId });
                }

                await _doctorRepository.UpdateAsync(doctor);
            }
        }

        public async Task<IEnumerable<Clinic>> GetAllClinicsAsync()
        {
            return await _clinicRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Specialization>> GetAllSpecializationsAsync()
        {
            return await _specializationRepository.GetAllAsync();
        }
        public async Task DeleteDoctorAsync(int id)
        {
            await _doctorRepository.DeleteAsync(id);
        }
    }
}
