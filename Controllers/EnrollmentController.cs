using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RegistrationControl.Models;
using RegistrationControl.Models.ViewModels;
using RegistrationControl.Services;
using RegistrationControl.Services.Exceptions;

namespace RegistrationControl.Controllers
{
    public class EnrollmentController : Controller
    {
        private readonly EnrollmentService _enrollmentService;
        private readonly LiveService _liveService;
        private readonly RegisteredService _registeredService;

        public EnrollmentController(EnrollmentService enrollmentService, LiveService liveService, RegisteredService registeredService)
        {
            _enrollmentService = enrollmentService;
            _liveService = liveService;
            _registeredService = registeredService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _enrollmentService.FindAllAsync();
            return View(list);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _enrollmentService.FindByIdAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        public async Task<IActionResult> Create()
        {
            List<Live> lives = await _liveService.FindAllAsync();
            List<Registered> registereds = await _registeredService.FindAllAsync();
            EnrollmentFromViewModel viewModel = new EnrollmentFromViewModel { Lives = lives, Registereds = registereds };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                List<Live> lives = await _liveService.FindAllAsync();
                List<Registered> registereds = await _registeredService.FindAllAsync();
                EnrollmentFromViewModel viewModel = new EnrollmentFromViewModel { Enrollment = enrollment, Lives = lives, Registereds = registereds };
                return View(viewModel);
            }

            await _enrollmentService.InsertAsync(enrollment);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _enrollmentService.FindByIdAsync(id.Value);

            if (enrollment == null)
            {
                return NotFound();
            }

            List<Live> lives = await _liveService.FindAllAsync();
            List<Registered> registereds = await _registeredService.FindAllAsync();
            EnrollmentFromViewModel viewModel = new EnrollmentFromViewModel { Enrollment = enrollment, Lives = lives, Registereds = registereds };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                List<Live> lives = await _liveService.FindAllAsync();
                List<Registered> registereds = await _registeredService.FindAllAsync();
                EnrollmentFromViewModel viewModel = new EnrollmentFromViewModel { Enrollment = enrollment, Lives = lives, Registereds = registereds };
                return View(viewModel);
            }

            if (enrollment.Id != id)
            {
                return NotFound();
            }

            try
            {
                await _enrollmentService.UpdateAsync(enrollment);
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

            var item = await _enrollmentService.FindByIdAsync(id.Value);

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
                await _enrollmentService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException error)
            {
                throw new DbConcurrencyException(error.Message);
            }
        }
    }
}