namespace BookAuther.DTO.Book
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public DateTime PublishedDate { get; set; }
    }
}
