namespace Domain.Events.Departments
{
    public class DepartmentUpdatedEvent : BaseEvent
    {
        public DepartmentUpdatedEvent(Department department)
        {
            Department = department;
        }

        public Department Department { get; }
    }
}
