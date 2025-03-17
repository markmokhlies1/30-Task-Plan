using BookStore.Data;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Services.BookService
{
    public class BookService : IBookService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public BookService(ApplicationDbContext applicationDbContext) 
        {
            _applicationDbContext = applicationDbContext;
        }
        public void CreateNewBook(Book book)
        {
            _applicationDbContext.books.Add(book);
            _applicationDbContext.SaveChangesAsync();
        }

        public void DeleteBook(Book book)
        {
            _applicationDbContext.books.Remove(book);
            _applicationDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            var books= await _applicationDbContext.books.ToListAsync();
            return books;
        }

        public async Task<Book> GetBookById(int id)
        {
            var book = await _applicationDbContext.books.FirstOrDefaultAsync(x => x.Id == id);
            return book;
        }

        public void UpdateBook(Book book)
        {
            _applicationDbContext.Update(book);
            _applicationDbContext.SaveChanges();
        }
    }
}
