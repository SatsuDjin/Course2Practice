using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IClinicRepository
    {
        Task<IEnumerable<Clinic>> GetAllAsync();
        Task<Clinic> GetByIdAsync(int id);
        Task AddAsync(Clinic clinic);
        Task UpdateAsync(Clinic clinic);
        Task DeleteAsync(int id);
    }
}
