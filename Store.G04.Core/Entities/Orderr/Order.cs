﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G04.Core.Entities.Orderr
{
    public class Order:BaseEntity<int>
    {
        public string BuyerEmail  { get; set; }

        public DateTimeOffset OrderDate { get; set; }=DateTimeOffset.UtcNow;

        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public Address ShipingAddress { get; set; }

        public int DeliveryMethodId {  get; set; }//FK

        public DeliveryMethod DeliveryMethod { get; set; }

        public ICollection<OrderItem> Items { get; set; }

        public decimal SubTotal { get; set; }

        public Decimal GetTotal()=>SubTotal+DeliveryMethod.Cost;

        public string PaymentIntentId { get; set; }

         
    }
}