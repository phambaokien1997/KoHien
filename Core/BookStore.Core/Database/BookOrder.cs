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
        public decimal? Discount { get; set; }
        public int OrderDetailID { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; } // Số lượng sách trong đơn hàng
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        [ForeignKey("OrderDetailID")]
        public OrderDetail OrderDetail { get; set; }
        [ForeignKey("BookId")]  // Tổng giá trị của đơn hàng
        public Book Book { get; set; }
        // Phương thức để tính toán tổng giá trị của đơn hàng từ OrderDetails
        public decimal TotalPrice
        {
            get
            {
                return Quantity * Book.Price;
            }
        }
        
    }
}

