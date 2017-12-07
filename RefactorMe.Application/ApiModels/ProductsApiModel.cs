using System.Collections.Generic;

namespace RefactorMe.Application.ApiModels
{
    public class ProductsApiModel
    {
        public IEnumerable<ProductApiModel> Items { get; set; }
    }
}
