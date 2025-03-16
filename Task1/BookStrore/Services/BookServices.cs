using BookStrore.Data;
using BookStrore.Model;
using Microsoft.EntityFrameworkCore;

namespace BookStrore.Services
{
    public class BookServices : IBookServices
    {
        private readonly ApplicationDbContext _context;

        public BookServices(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task AddAsync(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var book = _context.Books.FirstOrDefault(x => x.Id == id);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _context.Books.ToListAsync();
        }

        public Task<Book> GetByIdAsync(int id)
        {
            return _context.Books.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }
    }
}
