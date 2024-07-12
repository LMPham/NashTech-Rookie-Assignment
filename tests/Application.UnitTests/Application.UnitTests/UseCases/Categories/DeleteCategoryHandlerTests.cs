using Application.UseCases.Categories.Commands.DeleteCategory;
using Ardalis.GuardClauses;
using Domain.Entities;
using FluentAssertions;

namespace Application.UnitTests.UseCases.Categories;

public class DeleteCategoryHandlerTests
{
    private readonly ApplicationDbContext dbContext;
    private readonly DeleteCategoryCommandHandler handler;
    public DeleteCategoryHandlerTests()
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
    [InlineData("Test Category 1", "Test Category Description 1")]
    [InlineData("Test Category 2", "Test Category Description 2")]
    [InlineData("Test Category 3", "Test Category Description 3")]
    public async Task GivenValidCommand_ShouldDeleteCategory_WhenCategoryExists(string name, string description)
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

        var command = new DeleteCategoryCommand { Id = category.Id };

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        var deletedCategory = await dbContext.FindAsync<Category>(category.Id);
        deletedCategory.Should().BeNull();
    }

    [Fact]
    public async Task GivenValidCommand_ShouldThrowNotFound_WhenCategoryNotExist()
    {
        // Arrange
        var command = new DeleteCategoryCommand { Id = 99 };

        // Act
        Func<Task> act = async () => await handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}
