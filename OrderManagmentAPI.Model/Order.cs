using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrderManagmentAPI.Model
{
    public class Order : EntityBase<int>
    {

        public Order(DateTime issueDate, IEnumerable<OrderItem> orderItems, Client client)
        {
            IssueDate = issueDate;
            OrderItems = orderItems;
            this.client = client;
            var status = "NewOrder";
            SetCount(status, 0);

            SetTotalValue(status, 0);
            Tax = (float)(TotalPrice * 0.03);
            SetFinalPrice();
        }

        protected Order()
        {
        }
        public float TotalPrice { get; private set; }
        public void SetTotalValue(string status, float OrderItemTotalPrice)
        {
            if (status == "Delete")
            {
                TotalPrice = this.TotalPrice - OrderItemTotalPrice;
            }
            if (status == "Add")
            {
                TotalPrice = this.TotalPrice + OrderItemTotalPrice;
            }
            else
                if (status == "CreateOrder")
            {
                TotalPrice = OrderItems.Select(x => x.TotalPrice).Sum();
            }
        }
        public DateTime IssueDate { get; set; }
        public float Tax { get; private set; }
        public Client client { get; set; }
        public int Count { get; private set; }
        public IEnumerable<OrderItem> OrderItems { get; set; }
        public Order AddOrderItemInCreationOrder(OrderItem newOrderItem)
        {
            List<OrderItem> orderItems = this.OrderItems.ToList();
            orderItems.Add(newOrderItem);
            var status = "CreateOrder";
            SetCount(status, newOrderItem.Count);

            SetTotalValue(status, newOrderItem.TotalPrice);
            Tax = (float)(TotalPrice * 0.03);
            newOrderItem.OrderId = id;
            SetFinalPrice();
            return (this);
        }
        public Order AddNewOrderItem(OrderItem newOrderItem)
        {
            var status = "Add";
            SetCount(status, newOrderItem.Count);

            SetTotalValue(status, newOrderItem.TotalPrice);
            Tax = (float)(TotalPrice * 0.03);

            SetFinalPrice();
            return (this);

        }
        public Order DeleteOrderItem(OrderItem DeletedOrderItem)
        {
            var status = "Delete";
            SetCount(status, DeletedOrderItem.Count);
            SetTotalValue(status, DeletedOrderItem.TotalPrice);
            Tax = (float)(TotalPrice * 0.03);
            SetFinalPrice();
            return (this);

        }
        public Order EditOrderItem(OrderItem OldOrderItem, OrderItem NewOrderItem)
        {

            Count = (this.Count - OldOrderItem.Count) + NewOrderItem.Count;

            TotalPrice = (this.TotalPrice - OldOrderItem.TotalPrice) + NewOrderItem.TotalPrice;
            Tax = (float)(TotalPrice * 0.03);

            SetFinalPrice();
            return (this);

        }
        public void SetCount(string status, int orderItemCount)
        {
            if (status == "Delete")
            {
                Count = this.Count - orderItemCount;
            }
            else
                if (status == "Add")
            {
                Count = this.Count + orderItemCount;
            }
            else
                if (status == "CreateOrder")
            {
                Count = OrderItems.Select(x => x.Count).Sum();
            }
        }
        public float FinalPrice { get; private set; }
        public void SetFinalPrice()
        {
            FinalPrice = TotalPrice + Tax;
        }
    }
}
