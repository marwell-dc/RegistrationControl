using RegistrationControl.Data;
using RegistrationControl.Models;
using Microsoft.EntityFrameworkCore;
using RegistrationControl.Services.Exceptions;

namespace RegistrationControl.Services
{
    public class InstructorService
    {
        private readonly RegistrationControlContext _context;

        public InstructorService(RegistrationControlContext context)
        {
            _context = context;
        }

        public async Task<List<Instructor>> FindAllAsync()
        {
            return await _context.Instructor.OrderBy(x => x.Name).ToListAsync();
        }

        public async Task<Instructor> FindById(int? id)
        {
            return await _context.Instructor.FirstOrDefaultAsync<Instructor>(x => x.Id == id);
        }

        public async Task InsertAsync(Instructor item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Instructor item)
        {
            bool hasAny = await _context.Instructor.AnyAsync(x => x.Id == item.Id);

            if (!hasAny)
            {
                throw new NotFoundException("Not found id.");
            }

            try
            {
                _context.Update<Instructor>(item);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException error)
            {
                throw new DbUpdateConcurrencyException(error.Message);
            }
        }

        public async Task RemoveAsync(int id)
        {
            try
            {
                var item = await _context.Instructor.FindAsync(id);
                _context.Instructor.Remove(item);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new IntegrityException("It is not possible to delete the Instructor because he/she is associated with a Live.");
            }
        }

        public async Task<bool> InstructorExists(int id)
        {
            return await _context.Instructor.AnyAsync(x => x.Id == id);
        }
    }
}
