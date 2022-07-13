using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;
using System.Text.Json.Serialization;

namespace Goomer_Lista_Rango.Data;

public class Restaurant
{
    private const string _bannedChar = "'*\"()[];@!";
    public int RestaurantId { get; internal set; }
    
    private string _name;
    public string Name
    {
        get => _name;
        internal set
        {
            if (value.Any(ch => _bannedChar.Contains(ch)))
                throw new BannedCharException();

            _name = value;
        }
    }
    public string? RestaurantPhoto { get; set; }
    public int AddressId { get; internal set; }
    public Address Address { get; internal set; }
    public IList<Product> Products { get; internal set; }
    public IList<OpenHour> OpenHours { get; internal set; }
}
