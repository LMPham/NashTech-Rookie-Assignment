using Application.Common.Models;
using Application.UseCases.Products.Queries.GetProduct;
using Ardalis.GuardClauses;
using AutoMapper;
using Domain.Entities;
using FluentAssertions;
using Moq;

namespace Application.UnitTests.UseCases.Products;

public class GetProductHandlerTests
{
    private readonly ApplicationDbContext dbContext;
    private readonly GetProductQueryHandler handler;
    private readonly Mock<IMapper> mapperMock;

    public GetProductHandlerTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        dbContext = new ApplicationDbContext(options);
        
        mapperMock = new Mock<IMapper>();

        handler = new(dbContext, mapperMock.Object);
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

    [Theory]
    [InlineData("Test Product 1", 1, 100000)]
    [InlineData("Test Product 2", 2, 200000)]
    [InlineData("Test Product 3", 3, 300000)]
    public async Task GivenValidCommand_ShouldGetProduct_WhenProductExists(
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

        var query = new GetProductQuery { Id = product.Id };

        mapperMock.Setup(m => m.Map<Product, ProductDto>(It.IsAny<Product>()))
            .Returns((Product p) => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Department = p.Department,
                Category = p.Category,
                Descriptions = p.Descriptions,
                Details = p.Details,
                Quantity = p.Quantity,
                Price = p.Price,
            });

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result!.Id.Should().Be(product.Id);
        result!.Name.Should().Be(product.Name);
        result!.Department.Id.Should().Be(product.Department.Id);
        result!.Category.Id.Should().Be(product.Category.Id);
        result!.Descriptions.Should().BeEquivalentTo(product.Descriptions);
        result!.Details.Should().BeEquivalentTo(product.Details);
        result!.Quantity.Should().Be(product.Quantity);
        result!.Price.Should().Be(product.Price);
    }

    [Fact]
    public async Task GivenValidQuery_ShouldThrowNotFound_WhenProductNotExist()
    {
        // Arrange
        var query = new GetProductQuery { Id = 99 };

        // Act
        Func<Task> act = async () => await handler.Handle(query, CancellationToken.None);

        // Arrange
        await act.Should().ThrowAsync<NotFoundException>();
    }
}
