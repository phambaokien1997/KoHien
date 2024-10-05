using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Core.Models
{
    public class AuthorItem
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? DateOfBirth { get; set; }
        public bool? Gender { get; set; }
        public string? Country { get; set; }
        public string? PhoneNumber { get; set; }
        public string[]? Books { get; set; }
        public int[] BookIds { get; set; }
        public string[]? Genres { get; set; }
        public int[] GenreIds { get; set; }
    }
}
