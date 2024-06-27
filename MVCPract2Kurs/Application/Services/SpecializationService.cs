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
    public class SpecializationService : ISpecializationService
    {
        private readonly ISpecializationRepository _specializationRepository;

        public SpecializationService(ISpecializationRepository specializationRepository)
        {
            _specializationRepository = specializationRepository;
        }

        public async Task<IEnumerable<Specialization>> GetAllSpecializationsAsync()
        {
            return await _specializationRepository.GetAllAsync();
        }

        public async Task<Specialization> GetSpecializationByIdAsync(int id)
        {
            return await _specializationRepository.GetByIdAsync(id);
        }

        public async Task AddSpecializationAsync(SpecializationViewModel model)
        {
            var specialization = new Specialization
            {
                Name = model.Name
            };

            await _specializationRepository.AddAsync(specialization);
        }

        public async Task UpdateSpecializationAsync(SpecializationViewModel model)
        {
            var specialization = await _specializationRepository.GetByIdAsync(model.SpecializationId);
            if (specialization != null)
            {
                specialization.Name = model.Name;
                await _specializationRepository.UpdateAsync(specialization);
            }
        }
        public async Task DeleteSpecializationAsync(int id)
        {
            await _specializationRepository.DeleteAsync(id);
        }
    }
}
