namespace Domain.Events.Products
{
    public class ProductDeletedEvent : BaseEvent
    {
        public ProductDeletedEvent(Product product)
        {
            Product = product;
        }

        public Product Product { get; }
    }
}
