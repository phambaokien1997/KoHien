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
        public DateTime OrderDate { get; set; }
        public string CustomerName { get; set; }
        public decimal TotalPrice { get; set; }
        
		public virtual ICollection<BookOrder> BookOrders { get; set; }
		
		public int Quantity { get; set; } // Số lượng sách trong đơn hàng
		public void CalculateTotalPrice()
		{
			TotalPrice = BookOrders.Sum(detail => detail.TotalPrice);
		}
		

	}
}
