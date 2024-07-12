
using Application.UseCases.Products.Commands.UpdateProduct;
using Ardalis.GuardClauses;
using Domain.Entities;
using FluentAssertions;

namespace Application.UnitTests.UseCases.Products;

public class UpdateProductHandlerTests
{
    private readonly ApplicationDbContext dbContext;
    private readonly UpdateProductCommandHandler handler;

    public UpdateProductHandlerTests()
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

    private static List<string>? CreateTestUpdatedDescriptions(
        List<string> descriptions, bool disableUpdateDescriptions)
    {
        if (disableUpdateDescriptions)
            return null;

        List<string> updatedDescriptions = new();
        foreach (var description in descriptions)
        {
            updatedDescriptions.Add($"Updated {description}");
        }
        return updatedDescriptions;
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

    private static List<ProductDetail>? CreateTestUpdatedProductDetails(
        List<ProductDetail> productDetails, bool disableUpdateProductDetails)
    {
        if (disableUpdateProductDetails)
            return null;

        List<ProductDetail> updatedProductDetails = new();
        foreach (var productDetail in  productDetails)
        {
            updatedProductDetails.Add(new ProductDetail
            {
                Name = $"Updated {productDetail.Name}",
                Description = $"Updated {productDetail.Description}",
            });
        }
        return updatedProductDetails;
    }

    [Theory]
    [InlineData("Test Product 1", 1, 100000, false, false, "Updated Product 1", 11, 100001)]
    [InlineData("Test Product 2", 2, 200000, false, false, null, 22, 200002)]
    [InlineData("Test Product 3", 3, 300000, false, false, "Updated Product 3", null, 300003)]
    [InlineData("Test Product 4", 4, 400000, false, false, "Updated Product 4", 44, null)]
    [InlineData("Test Product 5", 5, 500000, true, false, "Updated Product 5", 55, 500005)]
    [InlineData("Test Product 6", 6, 600000, false, true, "Updated Product 6", 66, 600006)]
    [InlineData("Test Product 7", 7, 700000, true, true, null, null, null)]
    public async Task GivenValidCommand_ShouldUpdateCategory_WhenCategoryExists(
        string name, int quantity, int price, bool disableUpdateDescriptions, bool disableUpdateProductDetails,
        string? updatedName, int? updatedQuantity, int? updatedPrice)
    {
        // Arrange
        var department = await CreateTestDepartmentAsync();
        var category = await CreateTestCategoryAsync(department);
        var descriptions = CreateTestDescriptions(name);
        var updatedDescriptions = CreateTestUpdatedDescriptions(descriptions, disableUpdateDescriptions);
        var details = CreateTestProductDetails(name);
        var updatedDetails = CreateTestUpdatedProductDetails(details, disableUpdateProductDetails);
        var product = new Product
        {
            Name = name,
            Department = department,
            Category = category,
            Descriptions = descriptions,
            Details = details,
            Quantity = quantity,
            Price = price,
        };
        await dbContext.AddAsync(product);
        await dbContext.SaveChangesAsync();

        var command = new UpdateProductCommand
        {
            Id = category.Id,
            Name = updatedName,
            Descriptions = updatedDescriptions,
            Details = updatedDetails,
            Quantity = updatedQuantity,
            Price = updatedPrice,
        };

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        var updatedProduct = await dbContext.FindAsync<Product>(product.Id);
        updatedProduct.Should().NotBeNull();

        if (updatedName != null)
        {
            updatedProduct!.Name.Should().Be(updatedName);
        }
        else
        {
            updatedProduct!.Name.Should().Be(name);
        }

        if (updatedDescriptions != null)
        {
            updatedProduct!.Descriptions.Should().BeEquivalentTo(updatedDescriptions);
        }
        else
        {
            updatedProduct!.Descriptions.Should().BeEquivalentTo(descriptions);
        }

        if (updatedDetails != null)
        {
            updatedProduct!.Details.Should().BeEquivalentTo(updatedDetails);
        }
        else
        {
            updatedProduct!.Details.Should().BeEquivalentTo(details);
        }

        if (updatedQuantity != null)
        {
            updatedProduct!.Quantity.Should().Be(updatedQuantity);
        }
        else
        {
            updatedProduct!.Quantity.Should().Be(quantity);
        }

        if (updatedPrice != null)
        {
            updatedProduct!.Price.Should().Be(updatedPrice);
        }
        else
        {
            updatedProduct!.Price.Should().Be(price);
        }
    }

    [Fact]
    public async Task GivenValidCommand_ShouldThrowNotFound_WhenProductNotExist()
    {
        // Arrange
        var command = new UpdateProductCommand { Id = 99 };

        // Act
        Func<Task> act = async () => await handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}
