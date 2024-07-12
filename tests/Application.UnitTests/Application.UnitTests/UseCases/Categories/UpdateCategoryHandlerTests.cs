using Application.UseCases.Categories.Commands.UpdateCategory;
using Ardalis.GuardClauses;
using Domain.Entities;
using FluentAssertions;

namespace Application.UnitTests.UseCases.Categories;

public class UpdateCategoryHandlerTests
{
    private readonly ApplicationDbContext dbContext;
    private readonly UpdateCategoryCommandHandler handler;
    public UpdateCategoryHandlerTests()
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

    [Theory]
    [InlineData("Test Category 1", "Test Category Description 1", "Updated Category 1", "Updated Description 1")]
    [InlineData("Test Category 2", "Test Category Description 2", null, "Updated Description 2")]
    [InlineData("Test Category 3", "Test Category Description 3", "Updated Category 3", null)]
    [InlineData("Test Category 3", "Test Category Description 3", null, null)]
    public async Task GivenValidCommand_ShouldUpdateCategory_WhenCategoryExists(
        string name, string description, string? updatedName, string? updatedDescription)
    {
        // Arrange
        var department = await CreateTestDepartmentAsync();
        var category = new Category
        {
            Name = name,
            Description = description,
            Department = department,
        };
        await dbContext.AddAsync(category);
        await dbContext.SaveChangesAsync();

        var command = new UpdateCategoryCommand
        {
            Id = category.Id,
            Name = updatedName,
            Description = updatedDescription,
        };

        // Act
        await handler.Handle(command, CancellationToken.None);

        var updatedCategory = await dbContext.FindAsync<Category>(category.Id);

        // Assert
        updatedCategory.Should().NotBeNull();

        if (updatedName != null)
        {
            updatedCategory!.Name.Should().Be(updatedName);
        }
        else
        {
            updatedCategory!.Name.Should().Be(name);
        }

        if (updatedDescription != null)
        {
            updatedCategory!.Description.Should().Be(updatedDescription);
        }
        else
        {
            updatedCategory!.Description.Should().Be(description);
        }
    }

    [Fact]
    public async Task GivenValidCommand_ShouldThrowNotFound_WhenCategoryNotExist()
    {
        // Arrange
        var command = new UpdateCategoryCommand { Id = 99 };

        // Act
        Func<Task> act = async () => await handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}
