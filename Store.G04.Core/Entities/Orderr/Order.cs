﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G04.Core.Entities.Orderr
{
    public class Order:BaseEntity<int>
    {
        public Order(string buyerEmail, AddressOrder shipingAddress,  DeliveryMethod deliveryMethod, ICollection<OrderItem> items, decimal subTotal, string paymentIntentId)
        {
            BuyerEmail = buyerEmail;
          
         
            ShipingAddress = shipingAddress;
           
            DeliveryMethod = deliveryMethod;
            Items = items;
            SubTotal = subTotal;
            PaymentIntentId = paymentIntentId;
        }

        public Order()
        {
        }

        public string BuyerEmail  { get; set; }

        public DateTimeOffset OrderDate { get; set; }=DateTimeOffset.UtcNow;

        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public AddressOrder ShipingAddress { get; set; }

        public int DeliveryMethodId {  get; set; }//FK

        public DeliveryMethod DeliveryMethod { get; set; }

        public ICollection<OrderItem> Items { get; set; }

        public decimal SubTotal { get; set; }

        public decimal GetTotal()=>SubTotal+DeliveryMethod.Cost;

        public string PaymentIntentId { get; set; }

         
    }
}
