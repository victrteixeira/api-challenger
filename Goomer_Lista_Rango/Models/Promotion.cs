using System.Text.Json.Serialization;

namespace Goomer_Lista_Rango.Data;

public class Promotion
{
    private const string _bannedChar = "'*\"()[];@";
    [JsonIgnore]
    public int DiscountId { get; internal set; }
    [JsonIgnore]
    public int ProductId { get; internal set; }
    public Discount Discount { get; internal set; }
    [JsonIgnore]
    public Product Product { get; internal set; }

    public string PromotionDescription { get; set; }
    public DateTime PromotionBegins { get; internal set; }
    public DateTime PromotionEnds { get; internal set; }
}