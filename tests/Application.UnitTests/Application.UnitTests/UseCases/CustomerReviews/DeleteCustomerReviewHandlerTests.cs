using Application.UseCases.CustomerReviews.Commands.DeleteCustomerReview;
using Ardalis.GuardClauses;
using Domain.Entities;
using FluentAssertions;

namespace Application.UnitTests.UseCases.CustomerReviews;

public class DeleteCustomerReviewHandlerTests
{
    private readonly ApplicationDbContext dbContext;
    private readonly DeleteCustomerReviewCommandHandler handler;

    public DeleteCustomerReviewHandlerTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        dbContext = new ApplicationDbContext(options);

        handler = new(dbContext);
    }

    private static List<string> CreateTestDescriptions(string productName)
    {
        List<string> descriptions =
        [
            $"{productName} Description 1",
            $"{productName} Description 2",
            $"{productName} Description 3",
        ];
        return descriptions;
    }

    private static List<ProductDetail> CreateTestProductDetails(string productName)
    {
        List<ProductDetail> productDetails =
        [
            new ProductDetail
            {
                Name = $"{productName} Detail 1",
                Description = $"{productName} Detail Description 1",
            },
            new ProductDetail
            {
                Name = $"{productName} Detail 2",
                Description = $"{productName} Detail Description 2",
            },
            new ProductDetail
            {
                Name = $"{productName} Detail 3",
                Description = $"{productName} Detail Description 3",
            },
        ];
        return productDetails;
    }

    private async Task<Product> CreateTestProductAsync()
    {
        var department = new Department
        {
            Name = "Test Department",
            Description = "Test Department Description",
        };

        var category = new Category
        {
            Name = "Test Category",
            Description = "Test Category Description",
        };

        var name = "Test Product";
        var product = new Product
        {
            Name = name,
            Department = department,
            Category = category,
            Descriptions = CreateTestDescriptions(name),
            Details = CreateTestProductDetails(name),
            Quantity = 1,
            Price = 100000,
        };

        await dbContext.AddAsync(product);
        await dbContext.SaveChangesAsync();
        return product;
    }

    [Theory]
    [InlineData(1, "Test Headline 1", "Test Comment 1")]
    [InlineData(2, "Test Headline 2", "Test Comment 2")]
    [InlineData(3, "Test Headline 3", "Test Comment 3")]
    public async Task GivenValidCommand_ShouldDeleteCustomerReview_WhenCustomerReviewExists(
        int score, string headline, string comment)
    {
        // Arrange
        var product = await CreateTestProductAsync();

        var customerReview = new CustomerReview
        {
            Score = score,
            Headline = headline,
            Comment = comment,
            Product = product,
        };
        await dbContext.AddAsync(customerReview);
        await dbContext.SaveChangesAsync();

        var command = new DeleteCustomerReviewCommand { Id = customerReview.Id };

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        var deletedCustomerReview = await dbContext.FindAsync<CustomerReview>(customerReview.Id);
        deletedCustomerReview.Should().BeNull();
    }

    [Fact]
    public async Task GivenValidCommand_ShouldThrowNotFound_WhenCustomerReviewNotExist()
    {
        // Arrange
        var command = new DeleteCustomerReviewCommand { Id = 99 };

        // Act
        Func<Task> act = async () => await handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}
