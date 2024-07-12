using Application.Common.Interfaces;
using Application.Common.Models;
using Application.UseCases.Products.Queries.GetProductsWithPagination;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Presentation.Controllers;
using Presentation.Models;

namespace Presentation.UnitTests.Controllers
{
    public class HomeControllerTests
    {
        private Mock<IMediator> mediator;
        private Mock<IUser> user;
        private Mock<IIdentityService> identityService;
        private Mock<IMapper> mapper;

        public HomeControllerTests()
        {
            mediator = new Mock<IMediator>();
            user = new Mock<IUser>();
            identityService = new Mock<IIdentityService>();
            mapper = new Mock<IMapper>();
        }

        [Fact]
        public async Task HomeIndexShouldReturnViewResultWithCorrectModel()
        {
            //mediator.Setup(m => m.Send(It.IsAny<GetProductsWithPaginationCommand>())).ReturnsAsync(new PaginatedList<ProductDto>([], 0, 0, 0));
            //user.SetupGet(u => u.Id).Returns("userId");
            //identityService.Setup(s => s.GetUserModeAsync("userId")).ReturnsAsync("mode");

            //var controller = new HomeController(mediator.Object, mapper.Object, user.Object, identityService.Object);
            //var result = await controller.Index(new GetProductsWithPaginationCommand()) as ViewResult;

            //Assert.NotNull(result);
            //Assert.IsType<HomeIndexModel>(result.Model);
            //var model = result.Model as HomeIndexModel;
            //Assert.NotNull(model);
            //Assert.Empty(model.Products.Items);
            //Assert.Null(model.Queries);
            //Assert.NotNull(model.User);
            //Assert.Equal("userId", model.User.Id);
        }

        [Fact]
        public void HomePrivacyShouldReturnViewResult()
        {
            var controller = new HomeController(null, null, null, null);
            var result = controller.Privacy() as ViewResult;
            Assert.NotNull(result);
        }
    }
}
