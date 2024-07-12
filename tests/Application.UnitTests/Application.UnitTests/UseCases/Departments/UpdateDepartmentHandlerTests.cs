using Application.UseCases.Departments.Commands.UpdateDepartment;
using Ardalis.GuardClauses;
using Domain.Entities;
using FluentAssertions;

namespace Application.UnitTests.UseCases.Departments;

public class UpdateDepartmentHandlerTests
{
    private readonly ApplicationDbContext dbContext;
    private readonly UpdateDepartmentCommandHandler handler;
    public UpdateDepartmentHandlerTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        dbContext = new ApplicationDbContext(options);

        handler = new(dbContext);
    }

    [Theory]
    [InlineData("Test Department 1", "Test Department Description 1", "Updated Department 1", "Updated Description 1")]
    [InlineData("Test Department 2", "Test Department Description 2", null, "Updated Description 2")]
    [InlineData("Test Department 3", "Test Department Description 3", "Updated Department 3", null)]
    [InlineData("Test Department 3", "Test Department Description 3", null, null)]
    public async Task GivenValidCommand_ShouldUpdateDepartment_WhenDepartmentExists(
        string name, string description, string? updatedName, string? updatedDescription)
    {
        // Arrange
        var department = new Department
        {
            Name = name,
            Description = description,
        };
        await dbContext.AddAsync(department);
        await dbContext.SaveChangesAsync();

        var command = new UpdateDepartmentCommand
        {
            Id = department.Id,
            Name = updatedName,
            Description = updatedDescription,
        };

        // Act
        await handler.Handle(command, CancellationToken.None);

        var updatedDepartment = await dbContext.FindAsync<Department>(department.Id);

        // Assert
        updatedDepartment.Should().NotBeNull();

        if(updatedName != null)
        {
            updatedDepartment!.Name.Should().Be(updatedName);
        }
        else
        {
            updatedDepartment!.Name.Should().Be(name);
        }

        if(updatedDescription != null)
        {
            updatedDepartment!.Description.Should().Be(updatedDescription);
        }
        else
        {
            updatedDepartment!.Description.Should().Be(description);
        }
    }

    [Fact]
    public async Task GivenValidCommand_ShouldThrowNotFound_WhenDepartmentNotExist()
    {
        // Arrange
        var command = new UpdateDepartmentCommand { Id = 99 };

        // Act
        Func<Task> act = async () => await handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}
