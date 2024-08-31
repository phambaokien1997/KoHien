using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Core.Database
{
    [Table("Books")]
    public class Book : BaseEntity
    {
        [MaxLength(256)]
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public int AuthorId { get; set; }
        public string Bardcode { get; set; }

        [ForeignKey("AuthorId")]
        public virtual Author Author { get; set; }
    }
}
