namespace Presentation.Controllers;

/// <summary>
/// Controller for managing CustomerReviews.
/// </summary>
[ApiController]
[Route("[controller]/[action]")]
public class CustomerReviewsController : Controller
{
    private readonly IMediator mediator;

    public CustomerReviewsController(IMediator _mediator)
    {
        mediator = _mediator;
    }

    [HttpGet(Name = "Swagger/GetCustomerReviews")]
    public async Task<PaginatedList<CustomerReview>> Get([FromQuery] GetCustomerReviewsWithPaginationQuery query)
    {
        return await mediator.Send(query);
    }

    [HttpGet(Name = "Swagger/GetCustomerReviewDetail")]
    [ActionName("GetDetail")]
    public async Task<CustomerReview> GetDetail([FromQuery] GetCustomerReviewQuery query)
    {
        return await mediator.Send(query);
    }

    [HttpPost(Name = "Swagger/CreateCustomerReview")]
    [Authorize]
    public async Task<int> Post(CreateCustomerReviewCommand command)
    {
        return await mediator.Send(command);
    }

    [HttpPatch(Name = "Swagger/UpdateCustomerReview")]
    [Authorize]
    public async Task<IResult> Patch(UpdateCustomerReviewCommand command)
    {
        await mediator.Send(command);
        return Results.NoContent();
    }

    [HttpDelete(Name = "Swagger/DeleteCustomerReview")]
    [Authorize]
    public async Task<IResult> Delete(DeleteCustomerReviewCommand command)
    {
        await mediator.Send(command);
        return Results.NoContent();
    }
}
