using Application.UseCases.Departments.Commands.CreateDepartment;
using Domain.Entities;
using FluentAssertions;

namespace Application.UnitTests.UseCases.Departments;

public class CreateDepartmentHandlerTests
{
    private readonly ApplicationDbContext dbContext;
    private readonly CreateDepartmentCommandHandler handler;
    public CreateDepartmentHandlerTests()
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
    public async Task GivenValidCommand_ShouldCreateDepartment(string name, string description)
    {
        // Arrange
        var command = new CreateDepartmentCommand
        {
            Name = name,
            Description = description
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        var department = await dbContext.FindAsync<Department>(result);

        // Assert
        department.Should().NotBeNull();
        department!.Name.Should().Be(name);
        department!.Description.Should().Be(description);
    }
}
