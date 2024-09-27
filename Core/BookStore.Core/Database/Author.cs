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
            Books = new HashSet<BookAuthor>();
            Genres = new HashSet<Genre>();
        }
        [MaxLength(256)]
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool? Gender { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }

        public virtual ICollection<BookAuthor> Books { get; set; }
        public virtual ICollection<Genre> Genres { get; set; }
    }
}
