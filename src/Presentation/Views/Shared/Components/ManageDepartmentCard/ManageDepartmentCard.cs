namespace Presentation.Views.Shared.Components.ManageDepartmentCard
{
    /// <summary>
    /// A view component for rendering cards that can be
    /// used to manage Departments.
    /// </summary>
    public class ManageDepartmentCard : ViewComponent
    {
        public IViewComponentResult Invoke(Department department)
        {
            return View(department);
        }
    }
}
