﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Core.Database
{
	[Table("Genre")]
	public class Genre : BaseEntity
	{
		public Genre() 
		{
			Books = new HashSet<Book>();
            AuthorGenres = new HashSet<AuthorGenre>();
            Name = string.Empty;
            Description = string.Empty; 
        }	
		public string Name { get; set; }
		public string Description { get; set; }
		public virtual ICollection<Book> Books { get; set; } 
		public virtual ICollection<AuthorGenre> AuthorGenres { get; set;}

	}
}
