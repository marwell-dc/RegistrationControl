using Microsoft.EntityFrameworkCore;
using RegistrationControl.Data;
using RegistrationControl.Models;
using RegistrationControl.Services.Exceptions;

namespace RegistrationControl.Services
{
    public class LiveService
    {
        private readonly RegistrationControlContext _context;

        public LiveService(RegistrationControlContext context)
        {
            _context = context;
        }

        public async Task<List<Live>> FindAllAsync()
        {

            return await _context.Live.Include(x => x.Instructor).ToListAsync();
        }

        public async Task<Live> FindByIdAsync(int? id)
        {
            return await _context.Live.Include(x => x.Instructor).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task InsertAsync(Live item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Live item)
        {
            bool hasAny = await _context.Live.AnyAsync(x => x.Id == item.Id);

            if (!hasAny)
            {
                throw new NotFoundException("Not found id.");
            }

            try
            {
                _context.Update<Live>(item);
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
                var item = await _context.Live.FindAsync(id);
                _context.Live.Remove(item);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException error)
            {
                throw new DbUpdateException(error.Message);
            }
        }

        public async Task<bool> LiveExists(int id)
        {
            return await _context.Live.AnyAsync(x => x.Id == id);
        }
    }
}