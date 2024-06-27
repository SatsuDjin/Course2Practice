using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IMainService
    {
        Task<IEnumerable<SpecializationViewModel>> GetSpecializationsWithDoctorCountsAsync(int? cityId);
        Task<IEnumerable<DoctorViewModel>> GetDoctorsBySpecializationAsync(int specializationId, int? cityId);
    }
}
