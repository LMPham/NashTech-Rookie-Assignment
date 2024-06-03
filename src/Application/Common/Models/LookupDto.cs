using Application.UseCases.Products.Commands.GetProductsWithPagination;
using AutoMapper;

namespace Application.Common.Models
{
    public class LookupDto
    {
        public int? DepartmentId { get; set; }
        public int? CategoryId { get; set; }
        public int? MinPrice { get; set; }

        public int? MaxPrice { get; set; }
        public string? Search { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 50;

        public bool HasFilterByDepartmentId()
        {
            return DepartmentId != null;
        }
        public bool HasFilterByCategoryId()
        {
            return CategoryId != null;
        }
        public bool HasFilterByPrice()
        {
            return MinPrice != null || MaxPrice != null;
        }
        public bool HasFilterBySearch()
        {
            return Search != null;
        }
        public bool HasFilterByCustomerReviewScore()
        {
            return false;
        }
        public bool HasFilter()
        {
            return HasFilterByDepartmentId()
                || HasFilterByCategoryId()
                || HasFilterByPrice()
                || HasFilterBySearch()
                || HasFilterByCustomerReviewScore();
        }

        public void ClearFilterByPrice()
        {
            MinPrice = null;
            MaxPrice = null;
        }

        public void ClearFilterByCategoryId()
        {
            CategoryId = null;
        }

        public void ClearFilterByCustomerReviewScore()
        {
            //
        }

        public IDictionary<string, string> ToDictionary()
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();

            if (DepartmentId != null)
            {
                dictionary.Add("DepartmentId", DepartmentId.ToString());
            }

            if (CategoryId != null)
            {
                dictionary.Add("CategoryId", CategoryId.ToString());
            }

            if (MinPrice != null)
            {
                dictionary.Add("MinPrice", MinPrice.ToString());
            }

            if (MaxPrice != null)
            {
                dictionary.Add("MaxPrice", MaxPrice.ToString());
            }

            if (Search != null)
            {
                dictionary.Add("Search", Search);
            }

            dictionary.Add("PageNumber", PageNumber.ToString());
            dictionary.Add("PageSize", PageSize.ToString());

            return dictionary;
        }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<GetProductsWithPaginationCommand, LookupDto>();
            }
        }
    }
}
