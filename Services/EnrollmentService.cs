using Microsoft.EntityFrameworkCore;
using RegistrationControl.Data;
using RegistrationControl.Models;
using RegistrationControl.Services.Exceptions;

namespace RegistrationControl.Services
{
    public class EnrollmentService
    {
        private readonly RegistrationControlContext _context;

        public EnrollmentService(RegistrationControlContext context)
        {
            _context = context;
        }

        public async Task<List<Enrollment>> FindAllAsync()
        {
            return await _context.Enrollment
                .Include(x => x.Live)
                .Include(x => x.Registered)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Enrollment> FindByIdAsync(int? id)
        {
            return await _context.Enrollment
                .Include(x => x.Live)
                .Include(x => x.Registered)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task InsertAsync(Enrollment item)
        {
            _context.Enrollment.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Enrollment item)
        {
            bool hasAny = await _context.Enrollment.AnyAsync(x => x.Id == item.Id);

            if (!hasAny)
            {
                throw new NotFoundException("Not found id.");
            }

            try
            {
                _context.Update<Enrollment>(item);
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
                var item = await _context.Enrollment.FindAsync(id);
                _context.Enrollment.Remove(item);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException error)
            {
                throw new DbConcurrencyException(error.Message);
            }
        }
    }
}