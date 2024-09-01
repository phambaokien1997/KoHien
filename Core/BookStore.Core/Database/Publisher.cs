using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Core.Database
{
	[Table("Publisher")]
	public class Publisher
	{
		public Publisher() 
		{
			Books = new HashSet<Book>();
		}
		public string Name { get; set; }
		public string Description { get; set; }
		public string Address { get; set; }
		public string ContactEmail { get; set; }
		public string PhoneNumber { get; set; }
		public virtual ICollection<Book> Books { get; set; }
	}
}
