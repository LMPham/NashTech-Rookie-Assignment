using AutoMapper;

namespace Application.UseCases.Products.Commands.GetProductsWithPagination
{
    public class ProductBriefDto
    {
        public int Id { get; init; }
        public string Name { get; init; } = "";
        public Department Department { get; init; } = new Department();
        public Category Category { get; init; } = new Category();
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
