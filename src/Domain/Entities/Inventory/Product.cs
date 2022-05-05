namespace RetailInMotion.Domain.Entities.Inventory
{ 
    public class Product : BaseEntity
    {
        public Product(int quantity)
        {
            _quantity = quantity;
        }

        private int _quantity;
        public int CurrentStock() => _quantity;

        public void RemoveUnits(int quantity)
        {
            _quantity -= quantity;
        }
        public void AddUnits(int quantity)
        {
            if (quantity < 0)
            {
                throw new OrderingDomainException("Invalid quantity");
            }
            _quantity += quantity;
        }
    }
}
