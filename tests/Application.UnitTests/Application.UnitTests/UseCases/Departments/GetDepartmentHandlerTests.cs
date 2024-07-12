using Application.UseCases.Departments.Queries.GetDepartment;
using Ardalis.GuardClauses;
using Domain.Entities;
using FluentAssertions;

namespace Application.UnitTests.UseCases.Departments;

public class GetDepartmentHandlerTests
{
    private readonly ApplicationDbContext dbContext;
    private readonly GetDepartmentQueryHandler handler;
    public GetDepartmentHandlerTests()
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
    public async Task GivenValidQuery_ShouldReturnDepartment_WhenDepartmentExists(string name, string description)
    {
        // Arrange
        var department = new Department
        {
            Name = name,
            Description = description,
        };
        await dbContext.AddAsync(department);
        await dbContext.SaveChangesAsync();

        var query = new GetDepartmentQuery { Id = department.Id };

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Should().Be(department);
    }

    [Fact]
    public async Task GivenValidQuery_ShouldThrowNotFound_WhenDepartmentNotExist()
    {
        // Arrange
        var query = new GetDepartmentQuery { Id = 99 };

        // Act
        Func<Task> act = async () => await handler.Handle(query, CancellationToken.None);

        // Arrange
        await act.Should().ThrowAsync<NotFoundException>();
    }
}
