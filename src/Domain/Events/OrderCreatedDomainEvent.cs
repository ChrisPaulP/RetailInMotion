namespace RetailInMotion.Domain.Events
{
    public class OrderCreatedDomainEvent : DomainEvent
    {
        public OrderCreatedDomainEvent(Order order)
        {
            Order = order;
        }

        public Order Order { get; }
    }
}
