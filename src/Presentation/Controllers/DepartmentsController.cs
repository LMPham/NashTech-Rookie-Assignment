using Application.Common.Models;

namespace Presentation.Controllers
{
    /// <summary>
    /// Controller for managing Departments.
    /// </summary>
    [ApiController]
    [Route("[controller]/[action]")]
    public class DepartmentsController : Controller
    {
        private readonly IMediator mediator;

        public DepartmentsController(IMediator _mediator)
        {
            mediator = _mediator;
        }

        [HttpPost(Name = "Swagger/CreateDepartment")]
        public async Task<int> Post(CreateDepartmentCommand command)
        {
            return await mediator.Send(command);
        }

        [HttpPatch(Name = "Swagger/UpdateDepartment")]
        public async Task<IResult> Patch(UpdateDepartmentCommand command)
        {
            await mediator.Send(command);
            return Results.NoContent();
        }

        [HttpDelete(Name = "Swagger/DeleteDepartment")]
        public async Task<IResult> Delete(DeleteDepartmentCommand command)
        {
            await mediator.Send(command);
            return Results.NoContent();
        }
    }
}
