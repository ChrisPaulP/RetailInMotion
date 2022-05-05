using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailInMotion.Domain.Events
{ 
    public class OrderItemAlteredDomainEvent: DomainEvent
    {
        public OrderItemAlteredDomainEvent(int productid, int quantity)
        {
            ProductId = productid;
            Quantity = quantity;
        }

        public int ProductId { get; }
        public int Quantity { get; }
    }
}
