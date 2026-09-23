
namespace WebAPI_BookManagement.Models.DTO
{
    public class addBookRequestDTO
    {
        public string Title { get; set; } //Tên sách

        public string Description { get; set; } //Mô tả sách

        public bool IsRead { get; set; } //Đã đọc hay chưa

        public DateTime? DateRead { get; set; } //Ngày đọc

        public int? Rate { get; set; } //Điểm đánh giá

        public string Genre { get; set; } //Thể loại

        public string CoverUrl { get; set; } //Đường dẫn ảnh bìa

        public DateTime DateAdded { get; set; } //Ngày thêm sách

        public int PublisherID { get; set; } //ID nhà xuất bản

        public List<int> AuthorIds { get; set; } //Danh sách ID tác giả
    }
}
