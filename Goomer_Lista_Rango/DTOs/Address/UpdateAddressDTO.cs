using System.ComponentModel.DataAnnotations;

namespace Goomer_Lista_Rango.DTOs;

public class UpdateAddressDTO
{
    [Required(ErrorMessage = $"Please provide a {nameof(Country)}")]
    [StringLength(30, MinimumLength = 2)]
    public string Country { get; set; }
    
    [Required(ErrorMessage = $"Please provide a {nameof(State)}")]
    [StringLength(40, MinimumLength = 3)]
    public string State { get; set; }
    
    [Required(ErrorMessage = $"Please provide a {nameof(City)}")]
    [StringLength(40, MinimumLength = 3)]
    public string City { get; set; }
    
    [Required(ErrorMessage = $"Please provide a {nameof(District)}")]
    [StringLength(50, MinimumLength = 2)]
    public string District { get; set; }
    
    [Required(ErrorMessage = $"Please provide a {nameof(Street)}")]
    [StringLength(40, MinimumLength = 3)]
    public string Street { get; set; }
    
    [Required(ErrorMessage = $"Please provide a {nameof(Number)}")]
    [StringLength(10, MinimumLength = 1)]
    public string Number { get; set; }
    
    [Required(ErrorMessage = $"Please provide a {nameof(ZipCode)}")]
    [StringLength(20, MinimumLength = 5)]
    public string ZipCode { get; set; }
}