namespace Domain.Events.Categories
{
    public class CategoryUpdatedEvent : BaseEvent
    {
        public CategoryUpdatedEvent(Category category)
        {
            Category = category;
        }

        public Category Category { get; }
    }
}
