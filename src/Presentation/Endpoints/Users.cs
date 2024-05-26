using Application.UseCases.Categories.Commands.CreateCategory;
using Infrastructure.Identity;

namespace Presentation.Endpoints
{
    /// <summary>
    /// User API endpoint group for handling User-related services.
    /// </summary>
    public class Users : EndpointGroupBase
    {
        public override void Map(WebApplication app)
        {
            app.MapGroup(this)
                .MapGet(GetNumber)
                .MapIdentityApi<ApplicationUser>();
        }

        public int GetNumber()
        {
            return 123456789;
        }
    }
}
