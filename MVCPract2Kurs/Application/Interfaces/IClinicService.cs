using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IClinicService
    {
        Task<IEnumerable<Clinic>> GetAllClinicsAsync();
        Task<Clinic> GetClinicByIdAsync(int id);
        Task AddClinicAsync(ClinicViewModel model);
        Task UpdateClinicAsync(ClinicViewModel model);
        Task DeleteClinicAsync(int id);
    }
}
