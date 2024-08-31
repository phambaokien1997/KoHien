using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Core.Database
{
    [Table("Authors")]
    public class Author : BaseEntity
    {
        public Author()
        {
            Books = new HashSet<Book>();
        }
        [MaxLength(256)]
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool? Gender { get; set; }
        public string Country { get; set; }
        public string Sdt { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
