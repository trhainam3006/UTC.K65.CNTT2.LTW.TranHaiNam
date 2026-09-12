using Microsoft.AspNetCore.Mvc.Rendering;

namespace BanSach.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public int GenreId { get; set; }
        public string Image { get; set; }
        public float Price { get; set; }
        public int TotalPage { get; set; }
        public string Sumary { get; set; }

        public List<Book> GetBookList()
        {
            List<Book> books = new List<Book>()
            {
                new Book(){
                    Id = 1,
                    Title = "Harry Potter",
                    AuthorId = 1,
                    GenreId = 1,
                    Image = "/images/products/b1.webp",
                    Price = 500000,
                    Sumary = "Tác phẩm nổi tiếng của J. K. Rowling.",
                    TotalPage = 250
                },
                new Book(){
                    Id = 2,
                    Title = "Chúa tể những chiếc nhẫn",
                    AuthorId = 2,
                    GenreId = 1,
                    Image = "/images/products/b2.webp",
                    Price = 450000,
                    Sumary = "Một thiên tiểu thuyết kiệt xuất của nhà văn J. R. R. Tolkien.",
                    TotalPage = 180
                },
                new Book(){
                    Id = 3,
                    Title = "Gone with the Wind (Cuốn Theo Chiều Gió) ",
                    AuthorId = 3,
                    GenreId = 1,
                    Image = "/images/products/b3.webp",
                    Price = 480000,
                    Sumary = "Một cuốn tiểu thuyết tình cảm của Margaret Mitchell.",
                    TotalPage = 220
                },
                new Book(){
                    Id = 4,
                    Title = "One Hundred Years of Solitude (Trăm năm cô đơn)",
                    AuthorId = 4,
                    GenreId = 1,
                    Image = "/images/products/b4.webp",
                    Price = 420000,
                    Sumary = "Cuốn tiểu thuyết nổi tiếng nhất của nhà văn người Colombia Gabriel Garcia Marquez.",
                    TotalPage = 200
                },
                new Book(){
                    Id = 5,
                    Title = "How to Win Friends and Influence People (Đắc nhân tâm)",
                    AuthorId = 5,
                    GenreId = 2,
                    Image = "/images/products/b5.webp",
                    Price = 550000,
                    Sumary = "Cuốn sách chia sẻ về những kỹ năng thu phục lòng người..",
                    TotalPage = 280
                },
                new Book(){
                    Id = 6,
                    Title = "Hoàng tử bé (Le Petit Prince)",
                    AuthorId = 6,
                    GenreId = 2,
                    Image = "/images/products/b6.webp",
                    Price = 600000,
                    Sumary = "Là cuốn truyện thiếu nhi nổi tiếng toàn cầu, nhiều người lớn đọc The Little Prince cũng phải ngẫm nghĩ nhiều mới hiểu.",
                    TotalPage = 300
                }
            };
            return books;
        }

        public Book GetBookById(int Id)
        {
            Book book = this.GetBookList().FirstOrDefault(b => b.Id == Id);
            return book;
        }

        public List<SelectListItem> Authors { get; } = new List<SelectListItem>
        {
            new SelectListItem {Value = "1", Text = "J.K. Rowling" },
            new SelectListItem {Value = "2", Text = "J.R.R. Tolkien" },
            new SelectListItem {Value = "3", Text = "Margaret Mitchell" },
            new SelectListItem {Value = "4", Text = "Gabriel Garcia Marquez" },
            new SelectListItem {Value = "5", Text = "Dale Carnegie" }
        };

        public List<SelectListItem> Genres { get; } = new List<SelectListItem>
        {
            new SelectListItem {Value = "1", Text = "Truyện ngắn" },
            new SelectListItem {Value = "2", Text = "Tiểu thuyết" },
            new SelectListItem {Value = "3", Text = "Hồi ký" },

        };
    }
}
