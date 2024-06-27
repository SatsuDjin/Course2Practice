using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ISpecializationRepository
    {
        Task<IEnumerable<Specialization>> GetAllAsync();
        Task<Specialization> GetByIdAsync(int id);
        Task AddAsync(Specialization specialization);
        Task UpdateAsync(Specialization specialization);
        Task DeleteAsync(int id);
    }
}
