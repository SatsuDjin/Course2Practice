using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;

        public CityService(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public async Task<IEnumerable<City>> GetAllCitiesAsync()
        {
            return await _cityRepository.GetAllAsync();
        }

        public async Task<City> GetCityByIdAsync(int id)
        {
            return await _cityRepository.GetByIdAsync(id);
        }

        public async Task AddCityAsync(CityViewModel model)
        {
            var city = new City
            {
                Name = model.Name
            };

            await _cityRepository.AddAsync(city);
        }

        public async Task UpdateCityAsync(CityViewModel model)
        {
            var city = await _cityRepository.GetByIdAsync(model.CityId);
            if (city != null)
            {
                city.Name = model.Name;
                await _cityRepository.UpdateAsync(city);
            }
        }
        public async Task DeleteCityAsync(int id)
        {
            await _cityRepository.DeleteAsync(id);
        }
    }
}
