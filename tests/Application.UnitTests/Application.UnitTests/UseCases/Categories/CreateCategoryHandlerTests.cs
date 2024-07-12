using Application.UseCases.Categories.Commands.CreateCategory;
using Ardalis.GuardClauses;
using Domain.Entities;
using FluentAssertions;

namespace Application.UnitTests.UseCases.Categories;

public class CreateCategoryHandlerTests
{
    private readonly ApplicationDbContext dbContext;
    private readonly CreateCategoryCommandHandler handler;

    public CreateCategoryHandlerTests()
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
    public async Task GivenValidCommand_ShouldCreateCategory(string name, string description)
    {
        // Arrange
        var department = await CreateTestDepartmentAsync();
        var command = new CreateCategoryCommand
        {
            Name = name,
            Description = description,
            Department = department,
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        var category = await dbContext.FindAsync<Category>(result);

        // Assert
        category.Should().NotBeNull();
        category!.Name.Should().Be(name);
        category!.Description.Should().Be(description);
        category!.Department.Should().Be(department);
    }

    [Fact]
    public async Task GivenValidCommand_ShouldThrowNotFound_WhenDepartmentNotExist()
    {
        var department = new Department
        {
            Id = 99,
            Name = "Test Department",
            Description = "Test Department Description",
        };
        var command = new CreateCategoryCommand
        {
            Name = "Test Category",
            Description = "Test Category Description",
            Department = department,
        };

        // Act
        Func<Task> act = async () => await handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}
