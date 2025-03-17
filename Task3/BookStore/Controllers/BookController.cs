using BookStore.DTO;
using BookStore.Models;
using BookStore.Services.BookService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/books")]
    [ApiController]
    [Authorize]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _bookService.GetAllBooks();

            var response = books.Select(book => new BookDto
            {
                PublishedDate = book.PublishedDate,
                Author = book.Author,
                Title = book.Title
            });

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var book = await _bookService.GetBookById(id);
            if (book == null)
                return NotFound(new { message = "Book not found" });

            return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody] BookDto bookDto)
        {
            if (bookDto == null)
                return BadRequest(new { message = "Invalid book data" });

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var book = new Book
            {
                Title = bookDto.Title,
                Author = bookDto.Author,
                PublishedDate = bookDto.PublishedDate
            };

            _bookService.CreateNewBook(book);

            return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, book);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] BookDto bookDto)
        {
            if (bookDto == null)
                return BadRequest(new { message = "Invalid book data" });

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingBook = _bookService.GetBookById(id).Result;
            if (existingBook == null)
                return NotFound(new { message = "Book not found" });

            existingBook.Title = bookDto.Title;
            existingBook.Author = bookDto.Author;
            existingBook.PublishedDate = bookDto.PublishedDate;

            _bookService.UpdateBook(existingBook);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = _bookService.GetBookById(id).Result;
            if (book == null)
                return NotFound(new { message = "Book not found" });

            _bookService.DeleteBook(book);
            return NoContent();
        }
    }
}
