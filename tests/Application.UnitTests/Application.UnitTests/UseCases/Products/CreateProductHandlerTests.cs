using Application.UseCases.Products.Commands.CreateProduct;
using Ardalis.GuardClauses;
using Domain.Entities;
using FluentAssertions;

namespace Application.UnitTests.UseCases.Products;

public class CreateProductHandlerTests
{
    private readonly ApplicationDbContext dbContext;
    private readonly CreateProductCommandHandler handler;

    public CreateProductHandlerTests()
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
    public async Task GivenValidCommand_ShouldCreateProduct(
        string name, int quantity, int price)
    {
        // Arrange
        var department = await CreateTestDepartmentAsync();
        var category = await CreateTestCategoryAsync(department);
        var command = new CreateProductCommand
        {
            Name = name,
            Department = department,
            Category = category,
            Descriptions = CreateTestDescriptions(name),
            Details = CreateTestProductDetails(name),
            Quantity = quantity,
            Price = price,
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        var product = await dbContext.FindAsync<Product>(result);
        product.Should().NotBeNull();
        product!.Name.Should().Be(name);
        product!.Department.Should().Be(department);
        product!.Category.Should().Be(category);
        product!.Descriptions.Should().BeEquivalentTo(command.Descriptions);
        product!.Details.Should().BeEquivalentTo(command.Details);
        product!.Quantity.Should().Be(quantity);
        product!.Price.Should().Be(price);
    }

    [Fact]
    public async Task GivenValidCommand_ShouldThrowNotFound_WhenDepartmentOrCategoryNotExist()
    {
        // Arrange
        var department = new Department
        {
            Id = 99,
            Name = "Test Department",
            Description = "Test Department Description",
        };

        var category = new Category
        {
            Id = 99,
            Name = "Test Category",
            Description = "Test Category Description",
        };

        var name = "Test Product";
        var command = new CreateProductCommand
        {
            Name = name,
            Department = department,
            Category = category,
            Descriptions = CreateTestDescriptions(name),
            Details = CreateTestProductDetails(name),
            Quantity = 1,
            Price = 100000,
        };

        // Act
        Func<Task> act = async () => await handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}
