using BookAuther.Model;

namespace BookAuther.Services.BookService
{
    public interface IBookRepository
    {
        Task<Book> CreateBookAsync(int authorId, Book book);
        Task<IEnumerable<Book>> GetBooksByAuthorIdAsync(int authorId);
        Task<Book?> GetBookByIdAsync(int authorId, int bookId);
        Task UpdateBookAsync(Book book);
        Task DeleteBookAsync(Book book);
    }
}
