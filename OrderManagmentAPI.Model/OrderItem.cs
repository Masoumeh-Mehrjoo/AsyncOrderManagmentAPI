using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OrderManagmentAPI.Model
{
    public class OrderItem : EntityBase<int>
    {
        protected OrderItem()
        {

        }
        public OrderItem(int orderId, int productId, float soldPrice, int count)
        {
            OrderId = orderId;
            ProductId = productId;
            SoldPrice = soldPrice;
            Count = count;
            SetTotalPrice();
        }

        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public float SoldPrice { get; set; }
        public int Count { get; set; }
        public float TotalPrice { get; private set; }

        public void SetTotalPrice()
        {
            TotalPrice = SoldPrice * Count;
        }
    }
}
