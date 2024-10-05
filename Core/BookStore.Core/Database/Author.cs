using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Core.Database
{
    [Table("Authors")]
    public class Author : BaseEntity
    {
        public Author()
        {
            BookAuthors = new HashSet<BookAuthor>();
            AuthorGenres = new HashSet<AuthorGenre>();
        }
        [MaxLength(256)]
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool? Gender { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }

        public virtual ICollection<BookAuthor> BookAuthors { get; set; }
        public virtual ICollection<AuthorGenre> AuthorGenres { get; set; }
    }
}
