using Microsoft.EntityFrameworkCore;

namespace ClientManagement.Infrastructure.Data.Repositories
{
    public class BaseRepository<T, TId> where T : class
    {
        private readonly AppDbContext _context;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<T?> GetByIdAsync(TId id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IList<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> SaveAsync(T t)
        {
            await _context.Set<T>().AddAsync(t);
            await _context.SaveChangesAsync();
            return t;
        }

        public async Task<T> UpdateAsync(T t)
        {
            _context.Entry(t).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return t;
        }

        public async Task<T?> DeleteAsync(TId id)
        {
            T? t = await GetByIdAsync(id);
            if (t != null)
            {
                _context.Entry(t).State = EntityState.Deleted;
                _context.Remove(t);
                await _context.SaveChangesAsync();
            }
            return t;
        }
    }
}
