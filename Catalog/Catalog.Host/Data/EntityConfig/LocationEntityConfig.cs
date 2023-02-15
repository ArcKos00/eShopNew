using Catalog.Host.Data.Entities;

namespace Catalog.Host.Data.EntityConfig
{
    public class LocationEntityConfig : IEntityTypeConfiguration<LocationEntity>
    {
        public void Configure(EntityTypeBuilder<LocationEntity> builder)
        {
            builder.ToTable("Location");
            builder.HasKey(k => k.Id);

            builder.Property(p => p.Id).UseHiLo("location_hilo").IsRequired();
        }
    }
}
