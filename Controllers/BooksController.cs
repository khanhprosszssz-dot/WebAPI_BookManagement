using Microsoft.AspNetCore.Mvc;
using WebAPI_BookManagement.Data;
using WebAPI_BookManagement.Models.DTO;
using WebAPI_BookManagement.Repositories;

namespace WebAPI_BookManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly IBookRepository _bookRepository;

        public BooksController(
            AppDbContext dbContext,
            IBookRepository bookRepository)
        {
            _dbContext = dbContext;
            _bookRepository = bookRepository;
        }

        [HttpGet("get-all-books")]
        public IActionResult GetAll()
        {
            var allBooks = _bookRepository.GetAllBooks();
            return Ok(allBooks);
        }

        [HttpGet]
        [Route("get-book-by-id/{id}")]
        public IActionResult GetBookById([FromRoute] int id)
        {
            var bookWithIdDTO = _bookRepository.GetBookById(id);
            return Ok(bookWithIdDTO);
        }

        [HttpPost("add-book")]
        public IActionResult AddBook(
            [FromBody] addBookRequestDTO addBookRequestDTO)
        {
            var bookAdd =
                _bookRepository.AddBook(addBookRequestDTO);

            return Ok(bookAdd);
        }

        [HttpPut("update-book-by-id/{id}")]
        public IActionResult UpdateBookById(
            int id,
            [FromBody] addBookRequestDTO bookDTO)
        {
            var updateBook =
                _bookRepository.UpdateBookById(id, bookDTO);

            return Ok(updateBook);
        }

        [HttpDelete("delete-book-by-id/{id}")]
        public IActionResult DeleteBookById(int id)
        {
            var deleteBook =
                _bookRepository.DeleteBookById(id);

            return Ok(deleteBook);
        }
    }
}