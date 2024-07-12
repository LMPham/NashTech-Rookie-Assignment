using AutoMapper;
using Domain.Common;

namespace Application.Common.Models;

public class ProductDto
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public Department Department { get; init; } = new Department();
    public Category Category { get; init; } = new Category();
    public List<string> Descriptions { get; set; } = [];
    public List<ProductDetail> Details { get; set; } = [];
    public List<CustomerReview> CustomerReviews { get; set; } = [];
    public CustomerReviewScoreSummary ScoreSummary { get; set; } = new CustomerReviewScoreSummary();
    public List<Image> Images { get; set; } = [];
    public int Quantity { get; set; }
    public int Price { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(
                    dest => dest.ScoreSummary,
                    opt => opt.MapFrom(src => CustomerReviewScoreSummary.Summarize(src))
                );
        }
    }
}
