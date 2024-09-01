using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Core.Database
{
	[Table("OrderDetail")]
	public class OrderDetail : BaseEntity
	{
		public int BookOrderId { get; set; }
		[ForeignKey("BookOrderId")]
		public BookOrder BookOrder { get; set; }
		public int BookId { get; set; }
		[ForeignKey("BookId")]
		public Book Book { get; set; }
		public int Quantity { get; set; } // Số lượng sách trong đơn hàng
		public decimal TotalPrice
		{
			get
			{
				return Quantity * Book.Price;
			}
		}

	}
}
