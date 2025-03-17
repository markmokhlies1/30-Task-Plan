using BookStore.Models;

namespace BookStore.Services.BookService
{
    public interface IBookService
    {
        public  void CreateNewBook(Book book);
        public  Task<IEnumerable<Book>> GetAllBooks();
        public Task<Book> GetBookById(int id);
        public void DeleteBook(Book book);
        public void UpdateBook(Book book);
    }
}
