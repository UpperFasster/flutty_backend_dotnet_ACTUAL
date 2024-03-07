using System.ComponentModel.DataAnnotations;
using fluttyBackend.Domain.Models;
using fluttyBackend.Domain.Models.ProductEntities;

namespace fluttyBackend.Service.services.ProductService.DTO.request
{
    public class NewProductDTO
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; init; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; init; }

        [Required(ErrorMessage = "Price is required")]
        public double Price { get; init; }
    }
}