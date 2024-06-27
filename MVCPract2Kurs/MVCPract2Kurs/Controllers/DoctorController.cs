using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using MVCPract2Kurs.Help;
using Domain.Entities;
using Application.DTOs;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Services;

namespace MVCPract2Kurs.Controllers
{
    [AdminAuthorize]
    public class DoctorController : Controller
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await PopulateDropdownLists();
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DoctorViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _doctorService.AddDoctorAsync(model);
                return RedirectToAction(nameof(Index));
            }

            await PopulateDropdownLists();
            return View(model);
        }

        
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var doctor = await _doctorService.GetDoctorByIdAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }

            var model = new DoctorViewModel
            {
                DoctorId = doctor.DoctorId,
                FullName = doctor.FullName,
                BirthDate = doctor.BirthDate,
                Description = doctor.Description,
                Photo = doctor.Photo,
                SelectedClinicIds = doctor.DoctorClinics.Select(dc => dc.ClinicId).ToList(),
                SelectedSpecializationIds = doctor.DoctorSpecializations.Select(ds => ds.SpecializationId).ToList()
            };

            await PopulateDropdownLists();
            return View(model);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DoctorViewModel model)
        {
            if (id != model.DoctorId)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                await _doctorService.UpdateDoctorAsync(model);
                return RedirectToAction(nameof(Index));
            }

            await PopulateDropdownLists();
            return View(model);
        }

        
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var doctors = await _doctorService.GetAllDoctorsAsync();
            return View(doctors);
        }

        
        private async Task PopulateDropdownLists()
        {
            var clinics = await _doctorService.GetAllClinicsAsync();
            var specializations = await _doctorService.GetAllSpecializationsAsync();
            ViewBag.Clinics = new SelectList(clinics, "ClinicId", "Name");
            ViewBag.Specializations = new SelectList(specializations, "SpecializationId", "Name");
        }
        
        public async Task<IActionResult> Delete(int id)
        {
            await _doctorService.DeleteDoctorAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
