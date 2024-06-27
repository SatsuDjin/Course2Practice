using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ISpecializationService
    {
        Task<IEnumerable<Specialization>> GetAllSpecializationsAsync();
        Task<Specialization> GetSpecializationByIdAsync(int id);
        Task AddSpecializationAsync(SpecializationViewModel model);
        Task UpdateSpecializationAsync(SpecializationViewModel model);
        Task DeleteSpecializationAsync(int id);
    }
}
