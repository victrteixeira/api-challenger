using Goomer_Lista_Rango.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Goomer_Lista_Rango.Mappings;

public class PromotionConfiguration : IEntityTypeConfiguration<Promotion>
{
    public void Configure(EntityTypeBuilder<Promotion> builder)
    {
        builder.HasKey(pp => new { pp.ProductId, pp.DiscountId });

        builder.HasOne<Product>(a => a.Product)
            .WithMany(b => b.Promotions)
            .HasForeignKey(fr => fr.ProductId);

        builder.HasOne<Discount>(a => a.Discount)
            .WithMany(b => b.Promotions)
            .HasForeignKey(fr => fr.DiscountId);
    }
}