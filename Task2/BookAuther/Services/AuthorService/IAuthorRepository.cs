using BookAuther.Model;
namespace BookAuther.Services.AuthorService
{
    public interface IAuthorRepository
    {
        Task<Author> CreateAuthorAsync(Author author);
        Task<IEnumerable<Author>> GetAllAuthorsAsync();
        Task<Author> GetAuthorByIdAsync(int id);
        Task UpdateAuthorAsync(Author author);
        Task DeleteAuthorAsync(int id);
        Task<bool> AuthorExists(int authorId);
    }
}
