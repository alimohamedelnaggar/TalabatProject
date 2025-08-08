using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.Order
{
    public class OrderItem:BaseEntity<int>
    {
        public OrderItem()
        {
            
        }

        public OrderItem(ProdcutItemOrder prodcut, decimal price, int quantity)
        {
            Prodcut = prodcut;
            Price = price;
            Quantity = quantity;
        }

        public ProdcutItemOrder Prodcut { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
