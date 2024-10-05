using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Core.Database
{
    [Table("Books")]
    public class Book : BaseEntity
    {
		public Book()
		{
			BookAuthors = new HashSet<BookAuthor>();
            BookOrders = new HashSet<BookOrder>();
            Name = string.Empty; 
            Title = string.Empty; 
            ShortDescription = string.Empty; 
            Price = 0; 
            Quantity = 0; 
            PublicationDate = DateTime.Now; 
        }
		[MaxLength(256)]
        
        public string Name { get; set; }
        
        public string Title { get; set; }
        
        public string ShortDescription { get; set; }
        
        public decimal Price { get; set; }
        
        public int Quantity { get; set; }
        
        public DateTime PublicationDate { get; set; }
		public int GenreId { get; set; }    
        [ForeignKey("GenreId")]
        public virtual Genre? Genre { get; set; }
		public int PublisherId { get; set; }
        [ForeignKey("PublisherId")]
        public virtual Publisher? Publisher { get; set; }
		public virtual ICollection<BookAuthor> BookAuthors { get; set; }
		public virtual ICollection<BookOrder>? BookOrders { get; set; }
	}
}
