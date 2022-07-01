using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RegistrationControl.Models;
using RegistrationControl.Services;
using RegistrationControl.Services.Exceptions;

namespace RegistrationControl.Controllers
{
    public class InstructorController : Controller
    {
        private readonly InstructorService _instructorService;

        public InstructorController(InstructorService instructorService)
        {
            _instructorService = instructorService;
        }

        // Controller da Index onde lista todos os item do banco
        public async Task<IActionResult> Index()
        {
            var list = await _instructorService.FindAllAsync();
            return View(list);
        }

        // Controller da pagina Create onde retorna o formulário
        public IActionResult Create()
        {
            return View();
        }

        // Controller do botão submit que efetivamente faz as
        // mudanças no banco de dados
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Instructor instructor)
        {
            if (!ModelState.IsValid)
            {
                return View(instructor);
            }

            await _instructorService.InsertAsync(instructor);
            return RedirectToAction(nameof(Index));
        }

        // Controller que retornas os detalhes pelo id
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _instructorService.FindById(id);

            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // Controller da pagina Edit onde retorna o formulário
        // já preenchido
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _instructorService.FindById(id.Value);

            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // Controller do botão que faz o submit Edit
        // edita efetivamente o item no banco com base no ID
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Instructor instructor)
        {
            if (id != instructor.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(instructor);
            }

            try
            {
                await _instructorService.UpdateAsync(instructor);
            }
            catch (DbUpdateConcurrencyException)
            {
                bool isExists = await _instructorService.InstructorExists(id);
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

        // Controller que retornas os detalhes pelo id para exclusão do item
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instructor = await _instructorService.FindById(id);

            if (instructor == null)
            {
                return NotFound();
            }

            return View(instructor);
        }

        // Controle que efetivamente deleta o item do 
        // banco de dados
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _instructorService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException error)
            {
                throw new IntegrityException(error.Message);
            }
        }

    }
}