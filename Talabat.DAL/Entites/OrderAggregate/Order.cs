using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Talabat
{
    public class Order:BaseEntity
    {

        public Order() { }
        public Order(string buyerEmail, OrderAddress shipToAddress, DeliveryMethod deliveryMethod, List<OrderItem> items, decimal subtotal)
        {
            BuyerEmail = buyerEmail;
            ShipToAddress = shipToAddress;
            DeliveryMethod = deliveryMethod;
            Items = items;
            Subtotal = subtotal;
        }

        public string BuyerEmail {  get; set; }

        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;

        public OrderAddress ShipToAddress { get; set; }

        public DeliveryMethod DeliveryMethod { get; set; }

        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public List<OrderItem> Items { get; set; }

        public int PaymentIntentId { get; set; }

        public decimal Subtotal {  get; set; }

        public decimal GetTotal() => Subtotal+DeliveryMethod.Cost;
    }
}
