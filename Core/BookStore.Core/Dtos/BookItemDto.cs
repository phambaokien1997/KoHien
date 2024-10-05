using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Core.Dtos
{
    public class BookItemDto
    {
        public string Name { get; set; }              
        public string Title { get; set; }              
        public decimal Price { get; set; }          
        public int GenreId { get; set; }             
        public int PublisherId { get; set; }            
        public int[] AuthorIds { get; set; }
    }
}
