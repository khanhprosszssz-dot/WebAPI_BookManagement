using WebAPI_BookManagement.Models.Domain;
using WebAPI_BookManagement.Models.DTO;

namespace WebAPI_BookManagement.Repositories
{
    public interface IPublisherRepository
    {
        List<PublisherDTO> GetAllPublishers();

        PublisherNoIdDTO GetPublisherById(int id);

        AddPublisherRequestDTO AddPublisher(
            AddPublisherRequestDTO addPublisherRequestDTO);

        PublisherNoIdDTO UpdatePublisherById(
            int id,
            PublisherNoIdDTO publisherNoIdDTO);

        Publisher? DeletePublisherById(int id);

        List<BookWithAuthorAndPublisherDTO> GetBooksByPublisherId(int id); //bai tap phan 4-5
    }
}