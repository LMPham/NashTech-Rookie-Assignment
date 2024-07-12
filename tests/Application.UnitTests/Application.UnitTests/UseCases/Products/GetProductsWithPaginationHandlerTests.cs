using Application.Common.Models;
using Application.UseCases.Products.Queries.GetProduct;
using Application.UseCases.Products.Queries.GetProductsWithPagination;
using AutoMapper;
using Domain.Common;
using Domain.Entities;
using Moq;

namespace Application.UnitTests.UseCases.Products;

public class GetProductsWithPaginationHandlerTests
{
    private readonly ApplicationDbContext dbContext;
    private readonly GetProductsWithPaginationQueryHandler handler;
    private readonly Mock<IMapper> mapperMock;

    public GetProductsWithPaginationHandlerTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        dbContext = new ApplicationDbContext(options);

        mapperMock = new Mock<IMapper>();

        handler = new(dbContext, mapperMock.Object);
    }

    private async Task<Department> CreateTestDepartmentAsync(int no)
    {
        var department = new Department
        {
            Name = $"Test Department {no}",
            Description = $"Test Department Description {no}",
        };
        await dbContext.AddAsync(department);
        await dbContext.SaveChangesAsync();

        return department;
    }

    private async Task<Category> CreateTestCategoryAsync(Department department, int no)
    {
        var category = new Category
        {
            Name = $"Test Category {no}",
            Description = $"Test Category Description {no}",
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

    private async Task<Product> CreateTestProductAsync(Department department, Category category, int no)
    {
        var name = $"Test Product {no}";
        var product = new Product
        {
            Name = name,
            Department = department,
            Category = category,
            Descriptions = CreateTestDescriptions(name),
            Details = CreateTestProductDetails(name),
            Quantity = 1 * no,
            Price = 100000 * no,
        };
        await dbContext.AddAsync(product);
        await dbContext.SaveChangesAsync();
        return product;
    }

    [Theory]
    [InlineData(null, null, null, null, null, null, 1, 20)]
    public async Task GivenValidCommand_ShouldDeleteProduct_WhenProductExists(
        int? departmentId, int? categoryId, int? minPrice, int? maxPrice, string? search,
        int? minCustomerReviewScore, int pageNumber, int pageSize)
    {
        // Arrange
        //Department[] departments =
        //{
        //    await CreateTestDepartmentAsync(1),
        //    await CreateTestDepartmentAsync(2),
        //    await CreateTestDepartmentAsync(3),
        //};

        //Category[] categories =
        //{
        //    await CreateTestCategoryAsync(departments[0], 1),
        //    await CreateTestCategoryAsync(departments[0], 2),
        //    await CreateTestCategoryAsync(departments[0], 3),
        //    await CreateTestCategoryAsync(departments[1], 4),
        //    await CreateTestCategoryAsync(departments[1], 5),
        //    await CreateTestCategoryAsync(departments[1], 6),
        //    await CreateTestCategoryAsync(departments[2], 7),
        //    await CreateTestCategoryAsync(departments[2], 8),
        //    await CreateTestCategoryAsync(departments[2], 9),
        //};

        //Product[] products =
        //{
        //    await CreateTestProductAsync(departments[0], categories[0], 1),
        //    await CreateTestProductAsync(departments[0], categories[0], 2),
        //    await CreateTestProductAsync(departments[0], categories[0], 3),
        //    await CreateTestProductAsync(departments[0], categories[1], 4),
        //    await CreateTestProductAsync(departments[0], categories[1], 5),
        //    await CreateTestProductAsync(departments[0], categories[1], 6),
        //    await CreateTestProductAsync(departments[0], categories[2], 7),
        //    await CreateTestProductAsync(departments[0], categories[2], 8),
        //    await CreateTestProductAsync(departments[0], categories[2], 9),
        //};

        //var query = new GetProductsWithPaginationQuery
        //{
        //    DepartmentId = departmentId,
        //    CategoryId = categoryId,
        //    MinPrice = minPrice,
        //    MaxPrice = maxPrice,
        //    Search = search,
        //    MinCustomerReviewScore = minCustomerReviewScore,
        //    PageNumber = pageNumber,
        //    PageSize = pageSize,
        //};

        //mapperMock.Setup(m => m.ConfigurationProvider).Returns(() => new MapperConfiguration(cfg =>
        //{
        //    cfg.CreateMap<Product, ProductDto>()
        //       .ForMember(
        //            dest => dest.ScoreSummary,
        //            opt => opt.MapFrom(src => CustomerReviewScoreSummary.Summarize(src))
        //        );
        //}));

        //mapperMock.Setup(m => m.Map<Product, ProductDto>(It.IsAny<Product>()))
        //    .Returns((Product p) => new ProductDto
        //    {
        //        Id = p.Id,
        //        Name = p.Name,
        //        Department = p.Department,
        //        Category = p.Category,
        //        Descriptions = p.Descriptions,
        //        Details = p.Details,
        //        Quantity = p.Quantity,
        //        Price = p.Price,
        //    });

        // Act
        //var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        //result.Should().NotBeNull();
        //result!.Id.Should().Be(product.Id);
        //result!.Name.Should().Be(product.Name);
        //result!.Department.Id.Should().Be(product.Department.Id);
        //result!.Category.Id.Should().Be(product.Category.Id);
        //result!.Descriptions.Should().BeEquivalentTo(product.Descriptions);
        //result!.Details.Should().BeEquivalentTo(product.Details);
        //result!.Quantity.Should().Be(product.Quantity);
        //result!.Price.Should().Be(product.Price);
    }
}
