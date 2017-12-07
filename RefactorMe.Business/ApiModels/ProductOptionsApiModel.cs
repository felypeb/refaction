using System.Collections.Generic;

namespace RefactorMe.Application.ApiModels
{
    public class ProductOptionsApiModel
    {
        public IEnumerable<ProductOptionApiModel> Items { get; set; }
    }
}
