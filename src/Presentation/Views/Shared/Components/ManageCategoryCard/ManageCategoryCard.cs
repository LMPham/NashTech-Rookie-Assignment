using Azure.Core;
using Microsoft.EntityFrameworkCore;

namespace Presentation.Views.Shared.Components.ManageCategoryCard
{
    /// <summary>
    /// A view component for rendering cards that can be
    /// used to manage Categories.
    /// </summary>
    public class ManageCategoryCard : ViewComponent
    {
        private readonly IApplicationDbContext dbContext;

        public ManageCategoryCard(IApplicationDbContext _dbContext)
        {
            dbContext = _dbContext;
        }
        public IViewComponentResult Invoke(Category category)
        {
            ViewData["products"] = dbContext.Products
                .Include(p => p.CustomerReviews)
                .Include(p => p.Details)
                .Include(p => p.Images)
                .Where(p => p.Category.Id == category.Id)
                .ToList();
            return View(category);
        }
    }
}
