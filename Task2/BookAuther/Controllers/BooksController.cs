using BookAuther.DTO.Book;
using BookAuther.Model;
using BookAuther.Services.AuthorService;
using BookAuther.Services.BookService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookAuther.Controllers
{
    [Route("api/authors/{authorId}/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;

        public BooksController(IBookRepository bookRepository,IAuthorRepository authorRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
        }
        [HttpPost]
        public async Task<IActionResult> CreateBook(int authorId, [FromBody] CreateBookDTO bookDto)
        {
            if (!await _authorRepository.AuthorExists(authorId))
                return NotFound(new { message = "Author not found" });

            var book = new Book
            {
                Title = bookDto.Title,
                PublishedDate = bookDto.PublishedDate,
            };
            var createdBook = await _bookRepository.CreateBookAsync(authorId,book);
            var bookResponse = new BookDto
            {
                Id = createdBook.Id,
                Title = createdBook.Title,
                PublishedDate = createdBook.PublishedDate,
                AuthorId = createdBook.AuthorId
            };

            return CreatedAtAction(nameof(GetBookById), new { authorId, bookId = createdBook.Id }, bookResponse);
        }
        [HttpGet]
        public async Task<IActionResult> GetBooksByAuthorId(int authorId)
        {
            if (!await _authorRepository.AuthorExists(authorId))
                return NotFound(new { message = "Author not found" });

            var books = await _bookRepository.GetBooksByAuthorIdAsync(authorId);
            var bookDtos = books.Select(book => new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                PublishedDate = book.PublishedDate,
                AuthorId = book.AuthorId
            });

            return Ok(bookDtos);
        }

        [HttpDelete("{bookId}")]
        public async Task<IActionResult> DeleteBook(int authorId, int bookId)
        {
            if (!await _authorRepository.AuthorExists(authorId))
                return NotFound(new { message = "Author not found" });

            var book = await _bookRepository.GetBookByIdAsync(authorId, bookId);
            if (book == null)
                return NotFound(new { message = "Book not found" });

            await _bookRepository.DeleteBookAsync(book);
            return NoContent();
        }

        [HttpPut("{bookId}")]
        public async Task<IActionResult> UpdateBook(int authorId, int bookId, [FromBody] CreateBookDTO bookDto)
        {
            if (!await _authorRepository.AuthorExists(authorId))
                return NotFound(new { message = "Author not found" });

            var book = await _bookRepository.GetBookByIdAsync(authorId, bookId);
            if (book == null)
                return NotFound(new { message = "Book not found" });

            book.Title = bookDto.Title;
            book.PublishedDate = bookDto.PublishedDate;

            await _bookRepository.UpdateBookAsync(book);
            return NoContent();
        }

        [HttpGet("{bookId}")]
        public async Task<IActionResult> GetBookById(int authorId, int bookId)
        {
            if (!await _authorRepository.AuthorExists(authorId))
                return NotFound(new { message = "Author not found" });

            var book = await _bookRepository.GetBookByIdAsync(authorId, bookId);
            if (book == null)
                return NotFound(new { message = "Book not found" });
            var bookDto = new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                PublishedDate = book.PublishedDate,
                AuthorId = book.AuthorId
            };

            return Ok(bookDto);
        }
    }
}
