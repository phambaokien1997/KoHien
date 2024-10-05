using System.ComponentModel.DataAnnotations;

namespace BookStore.Core.Models
{
    public class BookItem
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Title { get; set; }
        public string? ShortDescription { get; set; }
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }
        public string? PublicationDate { get; set; }
        public int GenreId { get; set; }
        public string? Genre { get; set; }
        public int PublisherId { get; set; }
        public string? Publisher { get; set; }
        public string[]? Authors { get; set; }
        public int[] AuthorIds { get; set; }
        public string CreatedAt { get; set; }
    }
}
