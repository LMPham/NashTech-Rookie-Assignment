using Application.UseCases.CustomerReviews.Queries.GetCustomerReviewsWithPagination;

namespace Presentation.Endpoints
{
    /// <summary>
    /// CustomerReviews API endpoint group for handling CustomerReview-related services.
    /// </summary>
    public class CustomerReviews : EndpointGroupBase
    {
        public override void Map(WebApplication app)
        {
            app.MapGroup(this)
                .MapGet(GetCustomerReviewsWithPagination)
                .MapGet(GetCustomerReview, "Detail/{id}")
                .MapPost(CreateCustomerReview)
                .MapPatch(UpdateCustomerReview, "{id}")
                .MapDelete(DeleteCustomerReview, "{id}");
        }

        public Task<PaginatedList<CustomerReview>> GetCustomerReviewsWithPagination(ISender sender, [AsParameters] GetCustomerReviewsWithPaginationCommand command)
        {
            return sender.Send(command);
        }

        public Task<CustomerReview> GetCustomerReview(ISender sender, int id)
        {
            var command = new GetCustomerReviewCommand { Id = id };
            return sender.Send(command);
        }

        public Task<int> CreateCustomerReview(ISender sender, CreateCustomerReviewCommand command)
        {
            return sender.Send(command);
        }

        public async Task<IResult> UpdateCustomerReview(ISender sender, int id, UpdateCustomerReviewCommand command)
        {
            if (id != command.Id)
            {
                return Results.BadRequest();
            }
            await sender.Send(command);
            return Results.NoContent();
        }

        public async Task<IResult> DeleteCustomerReview(ISender sender, int id)
        {
            await sender.Send(new DeleteCustomerReviewCommand { Id = id });
            return Results.NoContent();
        }
    }
}
