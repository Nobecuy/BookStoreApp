namespace BookStore8.Model.Book
{
    public class BookReadOnlyDto : BaseDto
    {
        public string Title { get; set; }

        public string image { get; set; }
        public decimal Price { get; set; }
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }     
    }
}
