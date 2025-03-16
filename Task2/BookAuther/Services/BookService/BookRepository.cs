using BookAuther.Data;
using BookAuther.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace BookAuther.Services.BookService
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _context;
        public BookRepository(ApplicationDbContext context)
        {
            _context=context;
        }

        public async Task<Book> CreateBookAsync(int authorId, Book book)
        {
            book.AuthorId = authorId;
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task DeleteBookAsync(Book book)
        {
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }

        public async Task<Book?> GetBookByIdAsync(int authorId, int bookId)
        {
            return await _context.Books
                .FirstOrDefaultAsync(b => b.Id == bookId && b.AuthorId == authorId);
        }

        public async Task<IEnumerable<Book>> GetBooksByAuthorIdAsync(int authorId)
        {
            return await _context.Books
            .Where(b => b.AuthorId == authorId)
            .ToListAsync();
        }

        public async Task UpdateBookAsync(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }
    }
}
