using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IDoctorService
    {
        Task<IEnumerable<Doctor>> GetAllDoctorsAsync();
        Task<Doctor> GetDoctorByIdAsync(int id);
        Task AddDoctorAsync(DoctorViewModel model);
        Task UpdateDoctorAsync(DoctorViewModel model);
        Task<IEnumerable<Clinic>> GetAllClinicsAsync();
        Task<IEnumerable<Specialization>> GetAllSpecializationsAsync();
        Task DeleteDoctorAsync(int id);
    }
}
