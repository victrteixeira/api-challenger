using System.ComponentModel.DataAnnotations;

namespace Goomer_Lista_Rango.DTOs;

public class CreateRestProdDTO
{
    // Restaurant DTO
    [Required(ErrorMessage = $"Please provide a {nameof(RestaurantName)} for Restaurant")]
    [StringLength(30, MinimumLength = 5)]
    public string RestaurantName { get; set; }
    public IFormFile RestaurantPhoto { get; set; }
    [Required(ErrorMessage = $"Please provide a {nameof(AddressId)} for Restaurant")]
    public int AddressId { get; set; }
    
    
    // Product DTO
    [Required(ErrorMessage = $"Please provide a {nameof(ProductName)} for Product")]
    [StringLength(30, MinimumLength = 3)]
    public string ProductName { get; set; }
    [Required(ErrorMessage = $"Please provide a {nameof(Category)}")]
    [StringLength(30, MinimumLength = 2)]
    public string Category { get; set; }
    [Required(ErrorMessage = $"Please provide a {nameof(Price)}")]
    public double Price { get; set; }
    
    public IFormFile ProductPhoto { get; set; }
    
     [Required(ErrorMessage = $"Please provide a {nameof(RestaurantId)}")]
     [StringLength(30, MinimumLength = 2)]
    public int RestaurantId { get; set; }
}