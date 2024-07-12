using Application.UseCases.Products.Commands.DeleteProduct;
using Ardalis.GuardClauses;
using Domain.Entities;
using FluentAssertions;

namespace Application.UnitTests.UseCases.Products;

public class DeleteProductHandlerTests
{
    private readonly ApplicationDbContext dbContext;
    private readonly DeleteProductCommandHandler handler;

    public DeleteProductHandlerTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        dbContext = new ApplicationDbContext(options);

        handler = new(dbContext);
    }

    private async Task<Department> CreateTestDepartmentAsync()
    {
        var department = new Department
        {
            Name = "Test Department",
            Description = "Test Department Description",
        };
        await dbContext.AddAsync(department);
        await dbContext.SaveChangesAsync();

        return department;
    }

    private async Task<Category> CreateTestCategoryAsync(Department department)
    {
        var category = new Category
        {
            Name = "Test Category",
            Description = "Test Category Description",
            Department = department,
        };
        await dbContext.AddAsync(category);
        await dbContext.SaveChangesAsync();

        return category;
    }

    private List<string> CreateTestDescriptions(string productName)
    {
        List<string> descriptions =
        [
            $"{productName} Description 1",
            $"{productName} Description 2",
            $"{productName} Description 3",
        ];
        return descriptions;
    }

    private List<ProductDetail> CreateTestProductDetails(string productName)
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

    [Theory]
    [InlineData("Test Product 1", 1, 100000)]
    [InlineData("Test Product 2", 2, 200000)]
    [InlineData("Test Product 3", 3, 300000)]
    public async Task GivenValidCommand_ShouldDeleteProduct_WhenProductExists(
        string name, int quantity, int price)
    {
        // Arrange
        var department = await CreateTestDepartmentAsync();
        var category = await CreateTestCategoryAsync(department);
        var product = new Product
        {
            Name = name,
            Department = department,
            Category = category,
            Descriptions = CreateTestDescriptions(name),
            Details = CreateTestProductDetails(name),
            Quantity = quantity,
            Price = price,
        };
        await dbContext.AddAsync(product);
        await dbContext.SaveChangesAsync();

        var command = new DeleteProductCommand { Id = product.Id };

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        var deletedProduct = await dbContext.FindAsync<Product>(product.Id);
        deletedProduct.Should().BeNull();
    }

    [Fact]
    public async Task GivenValidCommand_ShouldThrowNotFound_WhenProductNotExist()
    {
        // Arrange
        var command = new DeleteProductCommand { Id = 99 };

        // Act
        Func<Task> act = async () => await handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}
