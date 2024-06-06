namespace Application.Common.Interfaces
{
    /// <summary>
    /// Base interface for application database contexts for interacting with the database.
    /// </summary>
    public interface IApplicationDbContext
    {
        DbSet<Product> Products { get; }
        DbSet<Category> Categories { get; }
        DbSet<Department> Departments { get; }
        DbSet<CustomerReview> CustomerReviews { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
