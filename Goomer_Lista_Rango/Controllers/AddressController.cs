using Goomer_Lista_Rango.Data;
using Goomer_Lista_Rango.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Goomer_Lista_Rango.Controllers;

[ApiController]
[Route("{controller}")]
public class AddressController : ControllerBase
{
    private RestaurantContext _context;
    public AddressController(RestaurantContext context)
    {
        _context = context;
    }

    [HttpPost]
    public IActionResult AddAddress([FromBody] CreateAddressDTO addressDto)
    {
        Address newAddress = new();
        newAddress.Country = addressDto.Country;
        newAddress.State = addressDto.State;
        newAddress.City = addressDto.City;
        newAddress.District = addressDto.District;
        newAddress.Street = addressDto.Street;
        newAddress.Number = addressDto.Number;
        newAddress.ZipCode = addressDto.ZipCode;
        
        _context.Addresses.Add(newAddress);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetAddressById), new { Id = newAddress.AddressId }, newAddress);
    }

    
    [HttpGet]
    public IEnumerable<Address> GetAllAddresses()
    {
        return _context.Addresses.ToList();
    }

    
    [HttpGet("Get/{id:int}")]
    public IActionResult GetAddressById(int id)
    {
        var addressDto = _context.Addresses.Where(x => x.AddressId == id)
            .Select(c => new ReadAddressDTO()
            {
                AddressId = c.AddressId,
                Country = c.Country,
                State = c.State,
                City = c.City,
                District = c.District,
                Street = c.Street,
                Number = c.Number,
                ZipCode = c.ZipCode
            }).FirstOrDefault();
        
        
        if (addressDto == null)
            NotFound();

        return Ok(addressDto);
    }

    
    [HttpPut("Update/{id:int}")]
    public IActionResult UpdateAddress(int id, [FromBody] UpdateAddressDTO addressDto)
    {
        var address = _context.Addresses
            .Where(x => x.AddressId == id)
            .FirstOrDefault();
        if (address == null)
            return NotFound();

        address.Country = addressDto.Country;
        address.State = addressDto.State;
        address.City = addressDto.City;
        address.District = addressDto.District;
        address.Street = addressDto.Street;
        address.Number = addressDto.Number;
        address.ZipCode = addressDto.ZipCode;

        _context.Addresses.Update(address);
        _context.SaveChanges();
        return NoContent();
    }

    
    [HttpDelete("Delete/{id:int}")]
    public IActionResult DeleteAddress(int id)
    {
        var addressEntity = _context.Addresses.Where(x => x.AddressId == id).FirstOrDefault();
        if (addressEntity == null)
            return NotFound();

        _context.Addresses.Remove(addressEntity);
        _context.SaveChanges();
        return NoContent();
    }
}