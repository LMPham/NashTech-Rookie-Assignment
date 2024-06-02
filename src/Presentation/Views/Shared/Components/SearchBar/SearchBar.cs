

namespace Presentation.Views.Shared.Components.SearchBar
{
    public class SearchBar : ViewComponent
    {
        private readonly IApplicationDbContext dbContext;

        public SearchBar(IApplicationDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public IViewComponentResult Invoke(LookupDto queries)
        {
            ViewData["departments"] = dbContext.Departments.ToList();
            return View(queries);
        }
    }
}
