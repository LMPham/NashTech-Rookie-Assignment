using Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Presentation.Views.Shared.Components.FilterByCategory
{
    public class FilterByCategoryId : ViewComponent
    {
        private readonly IApplicationDbContext dbContext;

        public FilterByCategoryId(IApplicationDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public IViewComponentResult Invoke(HomeIndexModel model)
        {
            ViewData["categories"] = model.Products.Items
                .Select(p => p.Category)
                .Distinct(new BaseEntityEqualityComparer<Category>())
                .OrderBy(c => c.Department.Name)
                .ThenBy(c => c.Name)
                .ToList();

            if (model.Queries.HasFilterByCategoryId())
            {
                ViewData["selectedCategory"] = dbContext.Categories.Where(c => c.Id == model.Queries.CategoryId).FirstOrDefault();
            }

            return View(model.Queries);
        }
    }
}
