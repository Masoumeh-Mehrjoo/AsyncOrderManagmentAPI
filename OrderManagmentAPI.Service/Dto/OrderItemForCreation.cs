using System;
using System.Collections.Generic;
using System.Text;

namespace OrderManagmentAPI.Service.Dto
{
    public  class OrderItemForCreation
    {
        public float SoldPrice { get; set; }
        public int Count { get; set; }

        public int ProductId { get; set; }
        public float TotalPrice { get; private set; }


        public OrderItemForCreation(int productId, float soldPrice, int count)
        {
            ProductId = productId;
            SoldPrice = soldPrice;
            Count = count;
            SetTotalPrice();

        }
        public void SetTotalPrice()
        {
             TotalPrice = SoldPrice * Count;
           
        }

    }
}
