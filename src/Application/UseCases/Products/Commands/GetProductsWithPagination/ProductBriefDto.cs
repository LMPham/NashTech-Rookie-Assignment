using AutoMapper;

namespace Application.UseCases.Products.Commands.GetProductsWithPagination
{
    public class ProductBriefDto
    {
        public string Name { get; init; } = "";
        public string Description { get; init; } = "";
        public int Price { get; init; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Product, ProductBriefDto>();
            }
        }
    }
}
