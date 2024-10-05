using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Core.Dtos
{
    public class AuthorItemDto
    {
        public string Name { get; set; }
        public string DateOfBirth { get; set; }
        public bool Gender { get; set; }
        public int[] BookIds { get; set; }
        public int[] GenreId { get; set; }
        public string? Country { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
