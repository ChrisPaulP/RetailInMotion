namespace RetailInMotion.Domain.Entities
{
    public class Order : BaseEntity, IHasDomainEvent
    {
        private DateTime _orderDate;
        public DeliveryAddress DeliveryAddress { get; private set; }

        private readonly List<OrderItem> _orderItems;
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;


        private readonly List<DomainEvent> _domainEvents;
        public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents?.AsReadOnly();
        public bool IsCancelled => _isCancelled;
        private bool _isCancelled;

        protected Order()
        {
            _orderItems = new List<OrderItem>();
            _domainEvents = new List<DomainEvent>();
            _isCancelled = false;
        }
        public Order(DeliveryAddress deliveryAddress) : this()
        {
            DeliveryAddress = deliveryAddress;
            _orderDate = DateTime.UtcNow;


            AddOrderCreatedDomainEvent();
        }
        public void AddOrderItem(int productId, string productName, decimal itemPrice, int quantity = 1) 
        {
            var orderItem = new OrderItem(productId, productName, itemPrice, quantity);
            _orderItems.Add(orderItem);
        }
        //public void UpdateOrderItem(int productId, decimal itemPrice, int quantity = 1)
        public void UpdateOrderItem(OrderItem existingOrderForProduct, int quantity)
        {
            existingOrderForProduct.UpdateQuantity(quantity);
        }

        public void UpdateOrderDeliveryAddress(string street, string city, string country, string postCode)
        {
            DeliveryAddress = DeliveryAddress.Create(street, city, country, postCode);
        }
        public void CancelOrder()
        {
            if (_isCancelled)
            {
                throw new InvalidOperationException("Order has already been cancelled");
            }
            _isCancelled = true;

            AddOrderCancelledDomainEvent();
        }


        private void AddOrderCreatedDomainEvent()
        {
            var orderStartedDomainEvent = new OrderCreatedDomainEvent(this);

                _domainEvents?.Add(orderStartedDomainEvent);            
        }
        private void AddOrderCancelledDomainEvent()
        {
            var orderCancelledDomainEvent = new OrderCancelledDomainEvent(this);

            _domainEvents?.Add(orderCancelledDomainEvent);
        }
    }
}


