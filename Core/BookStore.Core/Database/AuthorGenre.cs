using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Core.Database
{
    [Table("AuthorGenres")]
    public class AuthorGenre
    {
        public int AuthorId { get; set; }
        public int GenreId { get; set; }
        [ForeignKey("AuthorId")]
        public virtual Author Author { get; set; }
        [ForeignKey("GenreId")]
        public virtual Genre Genre { get; set; }
    }
}
