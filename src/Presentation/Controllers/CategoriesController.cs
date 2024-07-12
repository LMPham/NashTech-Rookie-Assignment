
namespace Presentation.Controllers;

/// <summary>
/// Controller for managing Categories.
/// </summary>
[ApiController]
[Route("[controller]/[action]")]
public class CategoriesController : Controller
{
    private readonly IMediator mediator;

    public CategoriesController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost(Name = "Swagger/CreateCategory")]
    [Authorize(Roles = Roles.Administrator)]
    public async Task<int> Post(CreateCategoryCommand command)
    {
        return await mediator.Send(command);
    }

    [HttpPatch(Name = "Swagger/UpdateCategory")]
    [Authorize(Roles = Roles.Administrator)]
    public async Task<IResult> Patch(UpdateCategoryCommand command)
    {
        await mediator.Send(command);
        return Results.NoContent();
    }

    [HttpDelete(Name = "Swagger/DeleteCategory")]
    [Authorize(Roles = Roles.Administrator)]
    public async Task<IResult> Delete(DeleteCategoryCommand command)
    {
        await mediator.Send(command);
        return Results.NoContent();
    }
}
