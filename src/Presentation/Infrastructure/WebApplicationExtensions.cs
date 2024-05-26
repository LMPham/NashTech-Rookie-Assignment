using System.Reflection;

namespace Presentation.Infrastructure
{
    /// <summary>
    /// Extension of the <see cref="WebApplication"/> class for
    /// mapping API endpoints.
    /// </summary>
    public static class WebApplicationExtensions
    {
        /// <summary>
        /// Maps the specified API endpoint group to a route,
        /// which can be used to map the group's endpoints for easier management.
        /// </summary>
        public static RouteGroupBuilder MapGroup(this WebApplication app, EndpointGroupBase group)
        {
            var groupName = group.GetType().Name;

            return app
                .MapGroup($"/api/{groupName}")
                .WithGroupName(groupName)
                .WithTags(groupName)
                .WithOpenApi();
        }

        /// <summary>
        /// Maps all endpoint groups in the executing assembly.
        /// </summary>
        public static WebApplication MapEndpoints(this WebApplication app)
        {
            var endpointGroupType = typeof(EndpointGroupBase);

            var assembly = Assembly.GetExecutingAssembly();

            var endpointGroupTypes = assembly.GetExportedTypes()
                .Where(t => t.IsSubclassOf(endpointGroupType));

            foreach (var type in endpointGroupTypes)
            {
                if (Activator.CreateInstance(type) is EndpointGroupBase instance)
                {
                    instance.Map(app);
                }
            }

            return app;
        }
    }
}
