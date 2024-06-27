using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using MVCPract2Kurs.Help;
using MVCPract2Kurs.Models;
using Domain.Entities;
using Application.DTOs;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;

namespace MVCPract2Kurs.Controllers
{
    [AdminAuthorize]
    public class ClinicController : Controller
    {
        private readonly IClinicService _clinicService;
        private readonly ICityService _cityService;

        public ClinicController(IClinicService clinicService, ICityService cityService)
        {
            _clinicService = clinicService;
            _cityService = cityService;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Cities = await _cityService.GetAllCitiesAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClinicViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _clinicService.AddClinicAsync(model);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Cities = await _cityService.GetAllCitiesAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var clinic = await _clinicService.GetClinicByIdAsync(id);
            if (clinic == null)
            {
                return NotFound();
            }

            var model = new ClinicViewModel
            {
                ClinicId = clinic.ClinicId,
                Name = clinic.Name,
                CityId = clinic.CityId,
                Address = clinic.Address,
                Photo = clinic.Photo,
                Description = clinic.Description
            };

            ViewBag.Cities = await _cityService.GetAllCitiesAsync();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ClinicViewModel model)
        {
            if (id != model.ClinicId)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                await _clinicService.UpdateClinicAsync(model);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Cities = await _cityService.GetAllCitiesAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var clinics = await _clinicService.GetAllClinicsAsync();
            return View(clinics);
        }
        
        public async Task<IActionResult> Delete(int id)
        {
            await _clinicService.DeleteClinicAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
