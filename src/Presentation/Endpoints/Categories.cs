using Application.UseCases.Categories.Commands.CreateCategory;
using Application.UseCases.Categories.Commands.DeleteCategory;

namespace Presentation.Endpoints
{
    public class Categories : EndpointGroupBase
    {
        public override void Map(WebApplication app)
        {
            app.MapGroup(this)
                .MapPost(CreateCategory)
                .MapDelete(DeleteCategory, "{id}");
        }

        public Task<int> CreateCategory(ISender sender, CreateCategoryCommand command)
        {
            return sender.Send(command);
        }

        public async Task<IResult> DeleteCategory(ISender sender, int id)
        {
            await sender.Send(new DeleteCategoryCommand { Id = id });
            return Results.NoContent();
        }
    }
}
