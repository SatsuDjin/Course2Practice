using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Data;
using Application.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Application.Interfaces;
using Domain.Interfaces;
using Infrastructure.Repositories;
using Infrastructure.Services;

namespace MVCPract2Kurs.Controllers
{
    [Authorize]
    public class MainController : Controller
    {
        private readonly IMainService _mainService;
        private readonly ISpecializationService _specializationService;
        private readonly IDoctorService _doctorService;
        private readonly ICityService _cityService;

        public MainController(IMainService mainService, ISpecializationService specializationService, IDoctorService doctorService, ICityService cityService)
        {
            _mainService = mainService;
            _specializationService = specializationService;
            _doctorService = doctorService;
            _cityService = cityService;
        }

        public async Task<IActionResult> Index(int? cityId)
        {
            var specializations = await _mainService.GetSpecializationsWithDoctorCountsAsync(cityId);

            ViewBag.Cities = await _cityService.GetAllCitiesAsync();
            ViewBag.SelectedCityId = cityId;

            return View(specializations);
        }

        public async Task<IActionResult> SpecializationDetails(int specializationId, int? cityId)
        {
            var doctors = await _mainService.GetDoctorsBySpecializationAsync(specializationId, cityId);

            ViewBag.Specialization = await _specializationService.GetSpecializationByIdAsync(specializationId);
            ViewBag.Cities = await _cityService.GetAllCitiesAsync();
            ViewBag.SelectedCityId = cityId;

            return View(doctors);
        }




        public IActionResult EditBD()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult City()
        {
            return RedirectToAction("Index", "City");
        }

        public IActionResult Clinic()
        {
            return RedirectToAction("Index", "Clinic");
        }

        public IActionResult Specialization()
        {
            return RedirectToAction("Index", "Specialization");
        }

        public IActionResult Doctor()
        {
            return RedirectToAction("Index", "Doctor");
        }
    }
}
