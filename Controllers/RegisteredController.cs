using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RegistrationControl.Models;
using RegistrationControl.Services;
using RegistrationControl.Services.Exceptions;

namespace RegistrationControl.Controllers
{
    public class RegisteredController : Controller
    {
        private readonly RegisteredService _registeredService;

        public RegisteredController(RegisteredService registeredService)
        {
            _registeredService = registeredService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _registeredService.FindAllAsync();
            return View(list);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Registered registered)
        {
            if (!ModelState.IsValid)
            {
                return View(registered);
            }
            await _registeredService.InsertAsyc(registered);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _registeredService.FindByIdAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return View(item);

        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _registeredService.FindByIdAsync(id.Value);

            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Registered registered)
        {
            if (id != registered.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(registered);
            }

            try
            {
                await _registeredService.UpdateAsync(registered);
            }
            catch (DbUpdateConcurrencyException)
            {
                bool isExists = await _registeredService.RegisteredExists(id);
                if (!isExists)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _registeredService.FindByIdAsync(id.Value);

            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _registeredService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException error)
            {
                throw new DbConcurrencyException(error.Message);
            }
        }
    }
}