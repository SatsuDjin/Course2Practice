using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Application.DTOs;
using Domain.Entities;
using System.Threading.Tasks;
using Infrastructure.Data;
using MVCPract2Kurs.Help;
using Application.Interfaces;

namespace MVCPract2Kurs.Controllers
{
    [AdminAuthorize]
    public class CityController : Controller
    {
        private readonly ICityService _cityService;

        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CityViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _cityService.AddCityAsync(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var city = await _cityService.GetCityByIdAsync(id);
            if (city == null)
            {
                return NotFound();
            }

            var model = new CityViewModel
            {
                CityId = city.CityId,
                Name = city.Name
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CityViewModel model)
        {
            if (id != model.CityId)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                await _cityService.UpdateCityAsync(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var cities = await _cityService.GetAllCitiesAsync();
            return View(cities);
        }
       
        public async Task<IActionResult> Delete(int id)
        {
            await _cityService.DeleteCityAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
