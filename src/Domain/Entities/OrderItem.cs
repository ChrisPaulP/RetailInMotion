namespace RetailInMotion.Domain.Entities
{
    public class OrderItem : BaseEntity, IHasDomainEvent
    {
        public int ProductId { get; private set; }

        private string _productName;
        private decimal _itemPrice;
        private int _quantity;
        private readonly List<DomainEvent> _domainEvents;
        public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents?.AsReadOnly();
        protected OrderItem() 
        {
            _domainEvents = new List<DomainEvent>();
        }

        public OrderItem(int productId, string productName, decimal itemPrice, int quantity = 1) : this()
        {
            ProductId = productId;
            _productName = productName;
            _itemPrice = itemPrice;
            _quantity = quantity;
        }
        public int GetQuantity() => _quantity;
        public string GetOrderItemProductName() => _productName;
        public decimal GetOrderItemPrice() => _itemPrice;      
        public void AddUnits(int quantity)
        {
            if (quantity < 0)
            {
                throw new OrderingDomainException("Invalid units");
            }

            _quantity += quantity;
        }
        public void RemoveUnits(int quantity)
        {
            if (quantity < 0)
            {
                throw new OrderingDomainException("There are no Items to remove");
            }

            _quantity -= quantity;
        }
        public void UpdateQuantity(int quantity)
        {
            if (quantity < 0)
            {
                throw new OrderingDomainException("Invalid quantity");
            }
            _quantity = quantity;
            AddOrderItemAlteredDomainEvent(quantity);
        }
        private void AddOrderItemAlteredDomainEvent(int quantity)
        {
            var orderItemAlteredDomainEvent = new OrderItemAlteredDomainEvent(this.ProductId, quantity);

            _domainEvents?.Add(orderItemAlteredDomainEvent);
        }
    }
}
