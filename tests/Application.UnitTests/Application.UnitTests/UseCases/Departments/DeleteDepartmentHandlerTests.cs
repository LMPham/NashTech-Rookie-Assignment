using Application.UseCases.Departments.Commands.DeleteDepartment;
using Ardalis.GuardClauses;
using Domain.Entities;
using FluentAssertions;

namespace Application.UnitTests.UseCases.Departments;

public class DeleteDepartmentHandlerTests
{
    private readonly ApplicationDbContext dbContext;
    private readonly DeleteDepartmentCommandHandler handler;
    public DeleteDepartmentHandlerTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        dbContext = new ApplicationDbContext(options);

        handler = new(dbContext);
    }

    [Theory]
    [InlineData("Test Department 1", "Test Department Description 1")]
    [InlineData("Test Department 2", "Test Department Description 2")]
    [InlineData("Test Department 3", "Test Department Description 3")]
    public async Task GivenValidCommand_ShouldDeleteDepartment_WhenDepartmentExists(string name, string description)
    {
        // Arrange
        var department = new Department
        {
            Name = name,
            Description = description,
        };
        await dbContext.AddAsync(department);
        await dbContext.SaveChangesAsync();

        var command = new DeleteDepartmentCommand { Id = department.Id };

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        var deletedDepartment = await dbContext.FindAsync<Department>(department.Id);
        deletedDepartment.Should().BeNull();
    }

    [Fact]
    public async Task GivenValidCommand_ShouldThrowNotFound_WhenDepartmentNotExist()
    {
        // Arrange
        var command = new DeleteDepartmentCommand { Id = 99 };

        // Act
        Func<Task> act = async () => await handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}
