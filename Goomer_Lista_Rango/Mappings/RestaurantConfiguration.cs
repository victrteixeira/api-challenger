using Goomer_Lista_Rango.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Goomer_Lista_Rango.Mappings;

public class RestaurantConfiguration : IEntityTypeConfiguration<Restaurant>
{
    public void Configure(EntityTypeBuilder<Restaurant> builder)
    {
        builder.HasKey(k => k.RestaurantId);

        builder.HasMany<OpenHour>(a => a.OpenHours)
            .WithOne(b => b.Restaurant)
            .HasForeignKey(fr => fr.RestaurantId);

        builder.HasOne<Address>(a => a.Address)
            .WithOne(b => b.Restaurant)
            .HasForeignKey<Restaurant>(fr => fr.AddressId);

        builder.HasMany<Product>(a => a.Products)
            .WithOne(b => b.Restaurant)
            .HasForeignKey(fr => fr.RestaurantId);
    }
}