using System;
using System.ComponentModel.DataAnnotations;

namespace RefactorMe.Application.ApiModels
{
    public class ProductApiModel
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public decimal Price { get; set; }

        public decimal DeliveryPrice { get; set; }
    }
}
