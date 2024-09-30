namespace Application.DAL
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public int PublicationYear { get; set; }
        public int AvailableCopies { get; set; }

        // Optional Relationships
        public int? AuthorId { get; set; }
        public Author Author { get; set; }

        public int? CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
