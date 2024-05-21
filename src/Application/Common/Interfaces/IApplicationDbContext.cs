namespace Application.Common.Interfaces
{
    // Base interface for application db context for interacting with the database.
    public interface IApplicationDbContext
    {
        DbSet<Product> Products { get; }
        DbSet<Category> Categories { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
