using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Goomer_Lista_Rango.Data;

public class Discount
{
    public int DiscountId { get; internal set; }
    public double DiscountPrice { get; internal set; }
    [JsonIgnore]
    public IList<Promotion> Promotions { get; internal set; }
}