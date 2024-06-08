using Application.Common.Behaviours;
using Application.Common.Interfaces;
using Application.UseCases.Departments.Commands.CreateDepartment;
using Microsoft.Extensions.Logging;
using Moq;

namespace Application.UnitTests.Common.Behaviors
{
    public class RequestLoggerTests
    {
        private Mock<ILogger<CreateDepartmentCommand>> logger = null!;
        private Mock<IUser> user = null!;
        private Mock<IIdentityService> identityService = null!;

        public RequestLoggerTests()
        {
            logger = new Mock<ILogger<CreateDepartmentCommand>>();
            user = new Mock<IUser>();
            identityService = new Mock<IIdentityService>();
        }

        [Fact]
        public async Task LoggerShouldCallGetUserNameAsyncOnceIfAuthenticated()
        {
            user.Setup(x => x.Id).Returns(Guid.NewGuid().ToString());

            var requestLogger = new LoggingBehaviour<CreateDepartmentCommand>(logger.Object, user.Object, identityService.Object);

            await requestLogger.Process(new CreateDepartmentCommand { Name = "Name", Description = "Description" }, new CancellationToken());

            identityService.Verify(i => i.GetUserNameAsync(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task LoggerShouldNotCallGetUserNameAsyncOnceIfUnauthenticated()
        {
            var requestLogger = new LoggingBehaviour<CreateDepartmentCommand>(logger.Object, user.Object, identityService.Object);

            await requestLogger.Process(new CreateDepartmentCommand { Name = "Name", Description = "Description" }, new CancellationToken());

            identityService.Verify(i => i.GetUserNameAsync(It.IsAny<string>()), Times.Never);
        }
    }
}
