namespace Presentation.Infrastructure
{
    /// <summary>
    /// Base class for API endpoint groups
    /// </summary>
    public abstract class EndpointGroupBase
    {
        /// <summary>
        /// Maps all endpoints of the API group
        /// </summary>
        public abstract void Map(WebApplication app);
    }
}
