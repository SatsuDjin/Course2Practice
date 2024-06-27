using Domain.Entities;
using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;

namespace Application.Interfaces
{
    public interface ICityService
    {
        Task<IEnumerable<City>> GetAllCitiesAsync();
        Task<City> GetCityByIdAsync(int id);
        Task AddCityAsync(CityViewModel model);
        Task UpdateCityAsync(CityViewModel model);
        Task DeleteCityAsync(int id);
        
    }
}
