using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using MVCPract2Kurs.Help;
using MVCPract2Kurs.Models;
using Domain.Entities;
using Application.DTOs;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Services;

namespace MVCPract2Kurs.Controllers
{
    [AdminAuthorize]
    public class SpecializationController : Controller
    {
        private readonly ISpecializationService _specializationService;

        public SpecializationController(ISpecializationService specializationService)
        {
            _specializationService = specializationService;
        }

        
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SpecializationViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _specializationService.AddSpecializationAsync(model);
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var specialization = await _specializationService.GetSpecializationByIdAsync(id);
            if (specialization == null)
            {
                return NotFound();
            }

            var model = new SpecializationViewModel
            {
                SpecializationId = specialization.SpecializationId,
                Name = specialization.Name
            };

            return View(model);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SpecializationViewModel model)
        {
            if (id != model.SpecializationId)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                await _specializationService.UpdateSpecializationAsync(model);
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var specializations = await _specializationService.GetAllSpecializationsAsync();
            return View(specializations);
        }
        
        public async Task<IActionResult> Delete(int id)
        {
            await _specializationService.DeleteSpecializationAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
