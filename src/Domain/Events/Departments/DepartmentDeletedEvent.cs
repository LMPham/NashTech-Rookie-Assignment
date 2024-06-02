namespace Domain.Events.Departments
{
    public class DepartmentDeletedEvent : BaseEvent
    {
        public DepartmentDeletedEvent(Department department)
        {
            Department = department;
        }

        public Department Department { get; }
    }
}
