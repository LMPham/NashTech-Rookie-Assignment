namespace Presentation.Endpoints
{
    /// <summary>
    /// Department API endpoint group for handling Department-related services.
    /// </summary>
    public class Departments : EndpointGroupBase
    {
        public override void Map(WebApplication app)
        {
            app.MapGroup(this)
                .MapPost(CreateDepartment)
                .MapPatch(UpdateDepartment, "{id}")
                .MapDelete(DeleteDepartment, "{id}");
        }

        public Task<int> CreateDepartment(ISender sender, CreateDepartmentCommand command)
        {
            return sender.Send(command);
        }

        public async Task<IResult> UpdateDepartment(ISender sender, int id, UpdateDepartmentCommand command)
        {
            if (id != command.Id)
            {
                return Results.BadRequest();
            }
            await sender.Send(command);
            return Results.NoContent();
        }

        public async Task<IResult> DeleteDepartment(ISender sender, int id)
        {
            await sender.Send(new DeleteDepartmentCommand { Id = id });
            return Results.NoContent();
        }
    }
}
