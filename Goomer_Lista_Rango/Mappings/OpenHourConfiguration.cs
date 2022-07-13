using Goomer_Lista_Rango.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Goomer_Lista_Rango.Mappings;

public class OpenHourConfiguration : IEntityTypeConfiguration<OpenHour>
{
    public void Configure(EntityTypeBuilder<OpenHour> builder)
    {
        builder.HasKey(k => k.OpenHourId);
    }
}