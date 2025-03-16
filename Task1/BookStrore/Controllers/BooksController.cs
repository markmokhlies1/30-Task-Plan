using BookStrore.Dto;
using BookStrore.Model;
using BookStrore.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStrore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookServices _bookServices;
        public BooksController(IBookServices bookServices)
        {
            _bookServices= bookServices;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            var books = await _bookServices.GetAllAsync();
            return Ok(books.Select(x=> new BookDto
            {
                Title = x.Title,
                Author = x.Author,
                PublishedDate = x.PublishedDate,
            }));
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<BookDto>> GetBook(int id)
        {
            var book =await _bookServices.GetByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(new BookDto
            {
                Title = book.Title,
                Author = book.Author,
                PublishedDate = book.PublishedDate,
            });
        }

        [HttpPost]
        public async Task<ActionResult<Book>> CreateBook(BookDto bookDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var book = new Book()
            {
                Title = bookDto.Title,
                Author = bookDto.Author,
                PublishedDate = bookDto.PublishedDate,
            };
            await _bookServices.AddAsync(book);
            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateBook(int id, BookDto bookDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var book = await _bookServices.GetByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            book.Title = bookDto.Title;
            book.Author = bookDto.Author;
            book.PublishedDate = bookDto.PublishedDate;

            await _bookServices.UpdateAsync(book);
            return NoContent();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleltBook(int id)
        {
            await _bookServices.DeleteAsync(id);
            return NoContent();
        }
    }
}
