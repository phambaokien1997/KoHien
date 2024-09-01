using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Core.Database
{
	[Table("BookOrders")]
	public class BookOrder : BaseEntity
	{
		public BookOrder()
		{
			OrderDetails = new HashSet<OrderDetail>();
		}

		public DateTime OrderDate { get; set; }
		public string CustomerName { get; set; }
		public decimal TotalPrice { get; set; }  // Tổng giá trị của đơn hàng

		public virtual ICollection<OrderDetail> OrderDetails { get; set; }

		// Phương thức để tính toán tổng giá trị của đơn hàng từ OrderDetails
		public void CalculateTotalPrice()
		{
			TotalPrice = OrderDetails.Sum(detail => detail.TotalPrice);
		}
	}
}

