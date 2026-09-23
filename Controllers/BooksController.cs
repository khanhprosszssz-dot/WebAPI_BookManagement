using Microsoft.EntityFrameworkCore;
using WebAPI_BookManagement.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebAPI_BookManagement.Data;

namespace WebAPI_BookManagement.Controllers
{
    [Route("api/[controller]")] //Xác định đường dẫn API
    [ApiController] //Đánh dấu đây là API Controller
    public class BooksController : ControllerBase //Lớp cơ sở của Controller
    {
        private readonly AppDbContext _dbContext; //Làm việc với database

        public BooksController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        [HttpGet("get-all-books")]
        public IActionResult GetAll()
        {
            // Get Data From Database - Domain Model
            var allBooksDomain = _dbContext.Books //Truy cập bảng Books
                .Include(b => b.Publisher) //Lấy thông tin nhà xuất bản
                .Include(b => b.Book_Authors) //Lấy thông tin tác giả thông qua bảng liên kết Book_Authors
                .ThenInclude(ba => ba.Author) //Lấy thông tin tác giả
                .ToList();

            // Map domain models to DTOs
            var allBooksDTO = allBooksDomain.Select(Books =>  //Chuyển Domain Model sang DTO
                new BookWithAuthorAndPublisherDTO()
                {
                    Id = Books.Id,
                    Title = Books.Title,
                    Description = Books.Description,
                    IsRead = Books.IsRead,
                    DateRead = Books.IsRead ? Books.DateRead : null,
                    Rate = Books.IsRead ? Books.Rate : null,
                    Genre = Books.Genre,
                    CoverUrl = Books.CoverUrl,
                    DateAdded = Books.DateAdded,
                    PublisherName = Books.Publisher.Name,
                    AuthorNames = Books.Book_Authors
                        .Select(n => n.Author.FullName)
                        .ToList()
                }).ToList(); //Chuyển kết quả thành danh sách

            return Ok(allBooksDTO); //Trả dữ liệu cho Client với HTTP 200
        }

        [HttpGet] //Khai báo phương thức HTTP GET
        [Route("get-book-by-id/{id:int}")] //Nhận ID là số nguyên từ URL
        public IActionResult GetBookById([FromRoute] int id) //Lấy ID từ đường dẫn
        {
            // Get bookDomain object from DB
            var bookDomain = _dbContext.Books
                .Include(b => b.Publisher)
                .Include(b => b.Book_Authors)
                .ThenInclude(ba => ba.Author)
                .FirstOrDefault(b => b.Id == id); //Tìm cuốn sách có ID tương ứng

            if (bookDomain == null) //Kiểm tra sách có tồn tại không
            {
                return NotFound(); //Trả HTTP 404 nếu không tìm thấy
            }

            // Map bookDomain object to DTO
            var bookDTO = new BookWithAuthorAndPublisherDTO()
            {
                Id = bookDomain.Id,
                Title = bookDomain.Title,
                Description = bookDomain.Description,
                IsRead = bookDomain.IsRead,
                DateRead = bookDomain.DateRead,
                Rate = bookDomain.Rate,
                Genre = bookDomain.Genre,
                CoverUrl = bookDomain.CoverUrl,
                DateAdded = bookDomain.DateAdded,

                PublisherName = bookDomain.Publisher != null
                    ? bookDomain.Publisher.Name
                    : "Unknown",

                AuthorNames = bookDomain.Book_Authors?
                    .Where(y => y.Author != null)
                    .Select(y => y.Author.FullName)
                    .ToList() ?? new List<string>()
            };

            return Ok(bookDTO); //Trả thông tin sách với HTTP 200
        }

        [HttpPost("add-book")] //Khai báo API thêm sách
        public IActionResult AddBook([FromBody] addBookRequestDTO addBookRequestDTO) //Nhận dữ liệu JSON từ Client
        {
            // check if publisher exists or not
            var publisherDomain = _dbContext.Publishers.FirstOrDefault(x => x.Id == //Tìm nhà xuất bản hoặc tác giả
                addBookRequestDTO.PublisherID);

            if (publisherDomain == null)
            {
                return NotFound(new { message = "Không tìm thấy NXB" }); //Trả về HTTP 404 nếu không tìm thấy
            }

            // create a new book domain object
            var bookDomain = new Models.Domain.Book()
            {
                Title = addBookRequestDTO.Title,
                Description = addBookRequestDTO.Description,
                IsRead = addBookRequestDTO.IsRead,
                DateRead = addBookRequestDTO.DateRead,
                Rate = addBookRequestDTO.Rate,
                Genre = addBookRequestDTO.Genre,
                CoverUrl = addBookRequestDTO.CoverUrl,
                DateAdded = addBookRequestDTO.DateAdded,
                PublisherID = publisherDomain.Id
            };

            _dbContext.Books.Add(bookDomain); //Thêm sách vào DbContext
            _dbContext.SaveChanges(); //Lưu thay đổi xuống SQL Server

            foreach (var authorId in addBookRequestDTO.AuthorIds) //Duyệt từng ID tác giả
            {
                var authorDomain = _dbContext.Authors.FirstOrDefault(x => x.Id == authorId);

                if (authorDomain == null)
                {
                    return NotFound(new { message = "Không tìm thấy tác giả" });
                }

                var bookAuthorDomain = new Models.Domain.Book_Author()
                {
                    BookId = bookDomain.Id,
                    AuthorId = authorDomain.Id
                };

                _dbContext.Books_Authors.Add(bookAuthorDomain); //Tạo liên kết giữa sách và tác giả
                _dbContext.SaveChanges();
            }

            return Ok(); //Trả về HTTP 200
        }

        // Viet cac action Post, Get, Update, Delete
    }
}
