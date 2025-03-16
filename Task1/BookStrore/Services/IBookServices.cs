using BookStrore.Model;

namespace BookStrore.Services
{
    public interface IBookServices
    {
        Task<IEnumerable<Book>> GetAllAsync();
        Task<Book> GetByIdAsync(int id);
        Task AddAsync(Book book);
        Task DeleteAsync(int id);
        Task UpdateAsync(Book book);
    }
}
