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

        private readonly IBookRepository
            _bookRepository;


        public BooksController(
            AppDbContext dbContext,
            IBookRepository bookRepository)
        {
            _dbContext = dbContext;

            _bookRepository =
                bookRepository;
        }


        // =====================================================
        // GET ALL + FILTER + SORT + PAGINATION
        // =====================================================

        [HttpGet("get-all-books")]
        public IActionResult GetAll(
            [FromQuery] string? filterOn,
            [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy,
            [FromQuery] bool isAscending = true,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 1000)
        {
            // su dung repository pattern
            var allBooks =
                _bookRepository.GetAllBooks(
                    filterOn,
                    filterQuery,
                    sortBy,
                    isAscending,
                    pageNumber,
                    pageSize);

            return Ok(allBooks);
        }


        // =====================================================
        // GET BOOK BY ID
        // =====================================================

        [HttpGet]
        [Route("get-book-by-id/{id}")]
        public IActionResult GetBookById(
            [FromRoute] int id)
        {
            var bookWithIdDTO =
                _bookRepository
                    .GetBookById(id);

            return Ok(bookWithIdDTO);
        }


        // =====================================================
        // ADD BOOK + VALIDATE
        // =====================================================

        [HttpPost("add-book")]
        public IActionResult AddBook(
            [FromBody]
            addBookRequestDTO addBookRequestDTO)
        {
            // Validate dữ liệu
            if (
                ValidateAddBook(
                    addBookRequestDTO))
            {
                var bookAdd =
                    _bookRepository
                        .AddBook(
                            addBookRequestDTO);

                return Ok(bookAdd);
            }

            return BadRequest(
                ModelState);
        }


        // =====================================================
        // UPDATE BOOK
        // =====================================================

        [HttpPut("update-book-by-id/{id}")]
        public IActionResult UpdateBookById(
            int id,
            [FromBody]
            addBookRequestDTO bookDTO)
        {
            var updateBook =
                _bookRepository
                    .UpdateBookById(
                        id,
                        bookDTO);

            return Ok(updateBook);
        }


        // =====================================================
        // DELETE BOOK
        // =====================================================

        [HttpDelete("delete-book-by-id/{id}")]
        public IActionResult DeleteBookById(
            int id)
        {
            var deleteBook =
                _bookRepository
                    .DeleteBookById(id);

            return Ok(deleteBook);
        }


        // =====================================================
        // VALIDATE ADD BOOK
        // =====================================================

        #region Private methods

        private bool ValidateAddBook(
            addBookRequestDTO addBookRequestDTO)
        {
            // Kiểm tra object null
            if (addBookRequestDTO == null)
            {
                ModelState.AddModelError(
                    nameof(
                        addBookRequestDTO),

                    "Please add book data");

                return false;
            }


            // Kiểm tra Description NotNull
            if (
                string.IsNullOrEmpty(
                    addBookRequestDTO
                        .Description))
            {
                ModelState.AddModelError(
                    nameof(
                        addBookRequestDTO
                            .Description),

                    $"{nameof(addBookRequestDTO.Description)} cannot be null");
            }


            // Kiểm tra Rate 0 - 5
            if (
                addBookRequestDTO.Rate < 0 ||
                addBookRequestDTO.Rate > 5)
            {
                ModelState.AddModelError(
                    nameof(
                        addBookRequestDTO
                            .Rate),

                    $"{nameof(addBookRequestDTO.Rate)} cannot be less than 0 and more than 5");
            }


            // Nếu có lỗi
            if (
                ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }

        #endregion
    }
}