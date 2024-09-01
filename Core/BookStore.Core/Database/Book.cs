using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Reflection.Metadata.BlobBuilder;

namespace BookStore.Core.Database
{
    [Table("Books")]
    public class Book : BaseEntity
    {
		public Book()
		{
			Authors = new HashSet<Author>();
			OrderDetails = new HashSet<OrderDetail>();
		}
		[MaxLength(256)]
        public string Name { get; set; }
        public string Title { get; set; }

        public string ShortDescription { get; set; }
        public int AuthorId { get; set; }
        public string Bardcode { get; set; }
		public decimal Price { get; set; }
		public DateTime PublicationDate { get; set; }
		public int GenreId { get; set; }
        [ForeignKey("GenreId")]
        public Genre Genre { get; set; }
		public int PublisherId { get; set; }
        [ForeignKey("PublisherId")]
        public Publisher Publisher { get; set; }
		public virtual ICollection<Author> Authors { get; set; }
		public virtual ICollection<OrderDetail> OrderDetails { get; set; }
	}
}
