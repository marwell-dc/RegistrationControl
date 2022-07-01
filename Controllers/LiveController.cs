using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RegistrationControl.Models;
using RegistrationControl.Models.ViewModels;
using RegistrationControl.Services;
using RegistrationControl.Services.Exceptions;

namespace RegistrationControl.Controllers
{
    public class LiveController : Controller
    {
        private readonly LiveService _liveService;
        private readonly InstructorService _instructorService;

        public LiveController(LiveService liveService, InstructorService instructorService)
        {
            _liveService = liveService;
            _instructorService = instructorService;
        }

        public async Task<IActionResult> Index()
        {
            var listLive = await _liveService.FindAllAsync();
            return View(listLive);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _liveService.FindByIdAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        public async Task<IActionResult> Create()
        {
            var instructor = await _instructorService.FindAllAsync();
            var viewModel = new LiveFromViewModel { Instructors = instructor };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Live live)
        {
            if (ModelState.IsValid)
            {
                List<Instructor> instructors = await _instructorService.FindAllAsync();
                LiveFromViewModel viewModel = new LiveFromViewModel { Live = live, Instructors = instructors };
                return View(viewModel);
            }

            await _liveService.InsertAsync(live);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var live = await _liveService.FindByIdAsync(id.Value);

            if (live == null)
            {
                return NotFound();
            }

            List<Instructor> instructors = await _instructorService.FindAllAsync();
            LiveFromViewModel viewModel = new LiveFromViewModel { Live = live, Instructors = instructors };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Live live)
        {
            if (ModelState.IsValid)
            {
                List<Instructor> instructors = await _instructorService.FindAllAsync();
                LiveFromViewModel viewModel = new LiveFromViewModel { Live = live, Instructors = instructors };
                return View(viewModel);
            }

            if (live.Id != id)
            {
                return NotFound();
            }

            try
            {
                await _liveService.UpdateAsync(live);
                return RedirectToAction(nameof(Index));
            }
            catch (DbConcurrencyException error)
            {
                throw new DbConcurrencyException(error.Message);
            }
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _liveService.FindByIdAsync(id.Value);

            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _liveService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException error)
            {
                throw new DbConcurrencyException(error.Message);
            }
        }
    }
}