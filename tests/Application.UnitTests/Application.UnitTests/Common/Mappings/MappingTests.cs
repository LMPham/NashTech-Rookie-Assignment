using Application.Common.Interfaces;
using Application.Common.Models;
using Application.UseCases.Products.Commands.GetProductsWithPagination;
using AutoMapper;
using Domain.Entities;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Application.UnitTests.Common.Mappings
{
    public class MappingTests
    {
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;

        public MappingTests()
        {
            _configuration = new MapperConfiguration(config =>
                config.AddMaps(Assembly.GetAssembly(typeof(IApplicationDbContext))));

            _mapper = _configuration.CreateMapper();
        }

        [Fact]
        public void ShouldHaveValidConfiguration()
        {
            _configuration.AssertConfigurationIsValid();
        }

        [Theory]
        [InlineData(typeof(GetProductsWithPaginationCommand), typeof(LookupDto))]
        [InlineData(typeof(Product), typeof(ProductDto))]
        public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
        {
            var instance = GetInstanceOf(source);

            _mapper.Map(instance, source, destination);
        }

        private object GetInstanceOf(Type type)
        {
            if (type.GetConstructor(Type.EmptyTypes) != null)
                return Activator.CreateInstance(type)!;

            // Type without parameterless constructor
            return RuntimeHelpers.GetUninitializedObject(type);
        }
    }
}
