using Microsoft.EntityFrameworkCore;
using RegistrationControl.Data;
using RegistrationControl.Models;
using RegistrationControl.Services.Exceptions;

namespace RegistrationControl.Services
{
    public class RegisteredService
    {
        private readonly RegistrationControlContext _context;

        public RegisteredService(RegistrationControlContext context)
        {
            _context = context;
        }

        public async Task<List<Registered>> FindAllAsync()
        {
            return await _context.Registered.ToListAsync();
        }

        public async Task<Registered> FindByIdAsync(int? id)
        {
            return await _context.Registered.FirstOrDefaultAsync<Registered>(x => x.Id == id);
        }

        public async Task InsertAsyc(Registered item)
        {
            _context.Add<Registered>(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Registered item)
        {
            bool hasAny = await _context.Registered.AnyAsync(x => x.Id == item.Id);

            if (!hasAny)
            {
                throw new NotFoundException("Not found id.");
            }
            try
            {
                _context.Update<Registered>(item);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException error)
            {
                throw new DbConcurrencyException(error.Message);
            }
        }

        public async Task RemoveAsync(int id)
        {
            try
            {
                var item = await _context.Registered.FindAsync(id);
                _context.Registered.Remove(item);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException error)
            {
                throw new DbConcurrencyException(error.Message);
            }
        }

        public async Task<bool> RegisteredExists(int id)
        {
            return await _context.Registered.AnyAsync(x => x.Id == id);
        }
    }
}