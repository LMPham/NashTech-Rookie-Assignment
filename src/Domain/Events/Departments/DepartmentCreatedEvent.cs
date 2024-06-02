namespace Domain.Events.Departments
{
    public class DepartmentCreatedEvent : BaseEvent
    {
        public DepartmentCreatedEvent(Department department)
        {
            Department = department;
        }

        public Department Department { get; }
    }
}
