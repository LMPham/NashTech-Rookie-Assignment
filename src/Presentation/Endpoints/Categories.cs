namespace Presentation.Endpoints
{
    /// <summary>
    /// Category API endpoint group for handling Category-related services.
    /// </summary>
    public class Categories : EndpointGroupBase
    {
        public override void Map(WebApplication app)
        {
            app.MapGroup(this)
                .MapPost(CreateCategory)
                .MapPatch(UpdateCategory, "{id}")
                .MapDelete(DeleteCategory, "{id}");
        }

        public Task<int> CreateCategory(ISender sender, CreateCategoryCommand command)
        {
            return sender.Send(command);
        }

        public async Task<IResult> UpdateCategory(ISender sender, int id, UpdateCategoryCommand command)
        {
            if (id != command.Id)
            {
                return Results.BadRequest();
            }
            await sender.Send(command);
            return Results.NoContent();
        }

        public async Task<IResult> DeleteCategory(ISender sender, int id)
        {
            await sender.Send(new DeleteCategoryCommand { Id = id });
            return Results.NoContent();
        }
    }
}
