namespace Domain.Events.Products
{
    public class ProductUpdatedEvent : BaseEvent
    {
        public ProductUpdatedEvent(Product product)
        {
            Product = product;
        }

        public Product Product { get; }
    }
}
