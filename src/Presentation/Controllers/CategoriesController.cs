using Application.UseCases.Categories.Commands.CreateCategory;
using Application.UseCases.Categories.Commands.DeleteCategory;
using Application.UseCases.Categories.Commands.UpdateCategory;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    /// <summary>
    /// Controller for managing Categories
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class CategoriesController : Controller
    {
        private readonly IMediator mediator;

        public CategoriesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost(Name = "Swagger/CreateCategory")]
        public async Task<int> Post(CreateCategoryCommand command)
        {
            return await mediator.Send(command);
        }

        [HttpPatch(Name = "Swagger/UpdateCategory")]
        public async Task<IResult> Patch(UpdateCategoryCommand command)
        {
            await mediator.Send(command);
            return Results.NoContent();
        }

        [HttpDelete(Name = "Swagger/DeleteCategory")]
        public async Task<IResult> Delete(DeleteCategoryCommand command)
        {
            await mediator.Send(command);
            return Results.NoContent();
        }
    }
}
