using Microsoft.EntityFrameworkCore;
using WebAPI_BookManagement.Data;
using WebAPI_BookManagement.Models.Domain;
using WebAPI_BookManagement.Models.DTO;

namespace WebAPI_BookManagement.Repositories
{
    public class SQLPublisherRepository : IPublisherRepository
    {
        private readonly AppDbContext _dbContext;

        public SQLPublisherRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }


//get all NXB
        public List<PublisherDTO> GetAllPublishers()
        {
            // Get Data From Database - Domain Model
            var allPublishersDomain =
                _dbContext.Publishers.ToList();

            // Map domain models to DTOs
            var allPublisherDTO =
                new List<PublisherDTO>();

            foreach (var publisherDomain in allPublishersDomain)
            {
                allPublisherDTO.Add(
                    new PublisherDTO()
                    {
                        Id = publisherDomain.Id,
                        Name = publisherDomain.Name
                    });
            }

            return allPublisherDTO;
        }


//get by id
        public PublisherNoIdDTO GetPublisherById(int id)
        {
            // Get Publisher Domain Model from Db
            var publisherWithIdDomain =
                _dbContext.Publishers
                    .FirstOrDefault(x => x.Id == id);

            if (publisherWithIdDomain != null)
            {
                // Map Domain Model to DTO
                var publisherNoIdDTO =
                    new PublisherNoIdDTO
                    {
                        Name =
                            publisherWithIdDomain.Name
                    };

                return publisherNoIdDTO;
            }

            return null;
        }


//add
        public AddPublisherRequestDTO AddPublisher(
            AddPublisherRequestDTO addPublisherRequestDTO)
        {
            var publisherDomainModel =
                new Publisher
                {
                    Name =
                        addPublisherRequestDTO.Name
                };

            // Use Domain Model to create Publisher
            _dbContext.Publishers.Add(
                publisherDomainModel);

            _dbContext.SaveChanges();

            return addPublisherRequestDTO;
        }


//update
        public PublisherNoIdDTO UpdatePublisherById(
            int id,
            PublisherNoIdDTO publisherNoIdDTO)
        {
            var publisherDomain =
                _dbContext.Publishers
                    .FirstOrDefault(n => n.Id == id);

            if (publisherDomain != null)
            {
                publisherDomain.Name =
                    publisherNoIdDTO.Name;

                _dbContext.SaveChanges();
            }

            return null;
        }


        //delete
        public Publisher? DeletePublisherById(int id)
        {
            var publisherDomain =
                _dbContext.Publishers
                    .FirstOrDefault(n => n.Id == id);

            if (publisherDomain != null)
            {
                _dbContext.Publishers.Remove(
                    publisherDomain);

                _dbContext.SaveChanges();
            }

            return null;
        }
//baitap phan 4-5
        public List<BookWithAuthorAndPublisherDTO>
    GetBooksByPublisherId(int id)
        {
            var books = _dbContext.Books
                .Where(b => b.PublisherID == id)
                .Select(book =>
                    new BookWithAuthorAndPublisherDTO()
                    {
                        Id = book.Id,
                        Title = book.Title,
                        Description = book.Description,
                        IsRead = book.IsRead,
                        DateRead = book.DateRead,
                        Rate = book.Rate,
                        Genre = book.Genre,
                        CoverUrl = book.CoverUrl,
                        DateAdded = book.DateAdded,

                        PublisherName = book.Publisher.Name,

                        AuthorNames = book.Book_Authors
                            .Select(ba => ba.Author.FullName)
                            .ToList()
                    })
                .ToList();

            return books;
        }
    }
}