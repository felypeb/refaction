using System;

namespace RefactorMe.Model.Entities
{
    public class ProductOption
    {
        public ProductOption()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
