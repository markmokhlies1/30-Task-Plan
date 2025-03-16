using BookAuther.DTO.Author;
using BookAuther.Model;
using BookAuther.Services.AuthorService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace BookAuther.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepository;
        public AuthorsController(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAuthor(CreateUpdatedAuthorDTO createAuthorDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var author = new Author
            {
                Name = createAuthorDTO.Name,
                BirthDate = createAuthorDTO.BirthDate
            };

            var createdAuthor = await _authorRepository.CreateAuthorAsync(author);

            var response = new AuthorDto
            {
                Id = createdAuthor.Id,
                Name = createdAuthor.Name,
                Birthdate = createdAuthor.BirthDate,
            };
            return CreatedAtAction(nameof(CreateAuthor),new {id = response.Id} ,response);
        }

        [HttpGet]
        public async Task<ActionResult> GetAllAuthors()
        {
            var authors = await _authorRepository.GetAllAuthorsAsync();

            var response = authors.Select(author =>
            new AuthorDto
            {
                Id = author.Id,
                Name = author.Name,
                Birthdate = author.BirthDate,
            });

            return Ok(response);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult> GetAuthorById(int id)
        {
            var author = await _authorRepository.GetAuthorByIdAsync(id);
            
            if (author == null)
            {
                return NotFound();
            }

            var response = new AuthorDto
            {
                Id = author.Id,
                Name = author.Name,
                Birthdate = author.BirthDate,
            };
            return Ok(response);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAuthor(int id, CreateUpdatedAuthorDTO author)
        {
            var existingAuthor = await _authorRepository.GetAuthorByIdAsync(id);

            if(existingAuthor == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            existingAuthor.Name = author.Name;
            existingAuthor.BirthDate = author.BirthDate;
            await _authorRepository.UpdateAuthorAsync(existingAuthor);

            return Ok(existingAuthor);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var existingAuthor = await _authorRepository.GetAuthorByIdAsync(id);

            if( existingAuthor == null)
            {
                return NotFound();
            }

            await _authorRepository.DeleteAuthorAsync(id);
            return Ok();
        }
    }
}
