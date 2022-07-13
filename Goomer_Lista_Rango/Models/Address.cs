using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Goomer_Lista_Rango.Data;

public class Address
{
    private const string _bannedChar = "'*\"()[];@!";
    public int AddressId { get; internal set; }
    
    private string _country;
    public string Country
    {
        get => _country;
        internal set
        {
            if (value.Any(ch => _bannedChar.Contains(ch)))
                throw new BannedCharException();

            _country = value;
        }
    }
    
    private string _state;
    public string State
    {
        get => _state;
        internal set
        {
            if (value.Any(ch => _bannedChar.Contains(ch)))
                throw new BannedCharException();

            _state = value;
        }
    }
    
    private string _city;
    public string City
    {
        get => _city;
        internal set
        {
            if(value.Any(ch => _bannedChar.Contains(ch)))
                throw new BannedCharException();

            _city = value;
        }
    }

    private string _district;
    public string District
    {
        get => _district;
        internal set
        {
            if (value.Any(ch => _bannedChar.Contains(ch)))
                throw new BannedCharException();

            _district = value;
        }
    }

    private string _street;
    public string Street
    {
        get => _street;
        internal set
        {
            if(value.Any(ch => _bannedChar.Contains(ch)))
                throw new BannedCharException();

            _street = value;
        }
    }

    private string _number;
    public string Number
    {
        get => _number;
        internal set
        {
            if(value.Any(ch => _bannedChar.Contains(ch)))
                throw new BannedCharException();

            _number = value;
        }
    }
    
    private string _zipcode;
    public string ZipCode
    {
        get => _zipcode;
        internal set
        {
            if(value.Any(ch => _bannedChar.Contains(ch)))
                throw new BannedCharException();

            _zipcode = value;
        }
    }
    [JsonIgnore]
    public Restaurant Restaurant { get; internal set; }
}
