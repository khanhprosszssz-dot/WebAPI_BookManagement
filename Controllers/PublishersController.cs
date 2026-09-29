using Microsoft.AspNetCore.Mvc;
using WebAPI_BookManagement.Data;
using WebAPI_BookManagement.Models.DTO;
using WebAPI_BookManagement.Repositories;

namespace WebAPI_BookManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        private readonly IPublisherRepository
            _publisherRepository;

        public PublishersController(
            AppDbContext dbContext,
            IPublisherRepository publisherRepository)
        {
            _dbContext = dbContext;
            _publisherRepository = publisherRepository;
        }


//get all nxb
        [HttpGet("get-all-publisher")]
        public IActionResult GetAllPublisher()
        {
            var allPublishers =
                _publisherRepository
                    .GetAllPublishers();

            return Ok(allPublishers);
        }


//get by id
        [HttpGet("get-publisher-by-id")]
        public IActionResult GetPublisherById(int id)
        {
            var publisherWithId =
                _publisherRepository
                    .GetPublisherById(id);

            return Ok(publisherWithId);
        }


//add
        [HttpPost("add-publisher")]
        public IActionResult AddPublisher(
            [FromBody]
            AddPublisherRequestDTO addPublisherRequestDTO)
        {
            var publisherAdd =
                _publisherRepository
                    .AddPublisher(
                        addPublisherRequestDTO);

            return Ok(publisherAdd);
        }


//update
        [HttpPut("update-publisher-by-id/{id}")]
        public IActionResult UpdatePublisherById(
            int id,
            [FromBody]
            PublisherNoIdDTO publisherDTO)
        {
            var publisherUpdate =
                _publisherRepository
                    .UpdatePublisherById(
                        id,
                        publisherDTO);

            return Ok(publisherUpdate);
        }


//delete
        [HttpDelete("delete-publisher-by-id/{id}")]
        public IActionResult DeletePublisherById(int id)
        {
            var publisherDelete =
                _publisherRepository
                    .DeletePublisherById(id);

            return Ok();
        }
//baitap phan 4-5
        [HttpGet("{id}/books")]
        public IActionResult GetBooksByPublisherId(int id)
        {
            var books =
                _publisherRepository.GetBooksByPublisherId(id);

            return Ok(books);
        }
    }
}