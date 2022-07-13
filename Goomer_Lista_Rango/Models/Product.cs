using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Goomer_Lista_Rango.Data;

public class Product
{
    public int ProductId { get; internal set; }
    private const string _bannedChar = "'*\"()[];@!";
    
    private string _name;
    public string Name
    {
        get => _name;
        internal set
        {
            if(value.Any(ch => _bannedChar.Contains(ch)))
                throw new BannedCharException();

            _name = value;
        }
    }
    
    public double Price { get; internal set; }

    private string _category;
    public string Category
    {
        get => _category;
        internal set
        {
            if (value.Any(ch => _bannedChar.Contains(ch)))
                throw new BannedCharException();

            _category = value;
        }
    }
    
    public string? ProductPhoto { get; internal set; }
    [JsonIgnore]
    public Restaurant Restaurant { get; internal set; }
    [JsonIgnore]
    public int RestaurantId { get; internal set; }
    public IList<Promotion> Promotions { get; internal set; }    
}