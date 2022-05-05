namespace RetailInMotion.Domain.Events
{
    public class OrderCancelledDomainEvent : DomainEvent
    {
        public OrderCancelledDomainEvent(Order order)
        {
            Order = order;
        }

        public Order Order { get; }
    }
}