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
    public class ClinicService : IClinicService
    {
        private readonly IClinicRepository _clinicRepository;

        public ClinicService(IClinicRepository clinicRepository)
        {
            _clinicRepository = clinicRepository;
        }

        public async Task<IEnumerable<Clinic>> GetAllClinicsAsync()
        {
            return await _clinicRepository.GetAllAsync();
        }

        public async Task<Clinic> GetClinicByIdAsync(int id)
        {
            return await _clinicRepository.GetByIdAsync(id);
        }

        public async Task AddClinicAsync(ClinicViewModel model)
        {
            var clinic = new Clinic
            {
                Name = model.Name,
                CityId = model.CityId,
                Address = model.Address,
                Photo = model.Photo,
                Description = model.Description
            };

            await _clinicRepository.AddAsync(clinic);
        }

        public async Task UpdateClinicAsync(ClinicViewModel model)
        {
            var clinic = await _clinicRepository.GetByIdAsync(model.ClinicId);
            if (clinic != null)
            {
                clinic.Name = model.Name;
                clinic.CityId = model.CityId;
                clinic.Address = model.Address;
                clinic.Photo = model.Photo;
                clinic.Description = model.Description;
                await _clinicRepository.UpdateAsync(clinic);
            }
        }
        public async Task DeleteClinicAsync(int id)
        {
            await _clinicRepository.DeleteAsync(id);
        }
    }
}
