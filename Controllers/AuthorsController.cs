using Microsoft.AspNetCore.Mvc;
using WebAPI_BookManagement.Data;
using WebAPI_BookManagement.Models.DTO;
using WebAPI_BookManagement.Repositories;

namespace WebAPI_BookManagement.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthorsController : ControllerBase
	{
		private readonly AppDbContext _dbContext;

		private readonly IAuthorRepository
			_authorRepository;

		public AuthorsController(
			AppDbContext dbContext,
			IAuthorRepository authorRepository)
		{
			_dbContext = dbContext;
			_authorRepository = authorRepository;
		}


//get all
		[HttpGet("get-all-author")]
		public IActionResult GetAllAuthor()
		{
			var allAuthors =
				_authorRepository
					.GellAllAuthors();

			return Ok(allAuthors);
		}


//get by id
		[HttpGet("get-author-by-id/{id}")]
		public IActionResult GetAuthorById(int id)
		{
			var authorWithId =
				_authorRepository
					.GetAuthorById(id);

			return Ok(authorWithId);
		}


//add
		[HttpPost("add-author")]
		public IActionResult AddAuthors(
			[FromBody]
			AddAuthorRequestDTO addAuthorRequestDTO)
		{
			var authorAdd =
				_authorRepository
					.AddAuthor(
						addAuthorRequestDTO);

			return Ok();
		}


//update
		[HttpPut("update-author-by-id/{id}")]
		public IActionResult UpdateBookById(
			int id,
			[FromBody]
			AuthorNoIdDTO authorDTO)
		{
			var authorUpdate =
				_authorRepository
					.UpdateAuthorById(
						id,
						authorDTO);

			return Ok(authorUpdate);
		}


//delete
		[HttpDelete("delete-author-by-id/{id}")]
		public IActionResult DeleteBookById(int id)
		{
			var authorDelete =
				_authorRepository
					.DeleteAuthorById(id);

			return Ok();
		}
//baitap phan 4-5
		[HttpGet("{id}/books")]
		public IActionResult GetBooksByAuthorId(int id)
		{
			var books =
				_authorRepository.GetBooksByAuthorId(id);

			return Ok(books);
		}
	}
}