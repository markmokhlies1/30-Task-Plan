using BookAuther.Data;
using BookAuther.Model;
using Microsoft.EntityFrameworkCore;

namespace BookAuther.Services.AuthorService
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly ApplicationDbContext _context;
        public AuthorRepository(ApplicationDbContext context)
        {
            _context=context;
        }

        public async Task<bool> AuthorExists(int authorId)
        {
            return await _context.Authors.AnyAsync(a  => a.Id == authorId);
        }

        public async Task<Author> CreateAuthorAsync(Author author)
        {
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
            return author;
        }

        public async Task DeleteAuthorAsync(int id)
        {
            var author = _context.Authors.FirstOrDefault(x => x.Id == id);
            if (author != null)
            {
                _context.Authors.Remove(author);
                await _context.SaveChangesAsync();   
            }
        }

        public async Task<IEnumerable<Author>> GetAllAuthorsAsync()
        {
            return await _context.Authors.ToListAsync();
        }

        public async Task<Author> GetAuthorByIdAsync(int id)
        {
            var author =await _context.Authors.FirstOrDefaultAsync(x =>x.Id == id);
            return author;
        }

        public async Task UpdateAuthorAsync(Author author)
        {
            _context.Authors.Update(author);
            await _context.SaveChangesAsync();
        }
    }
}
