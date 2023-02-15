using Catalog.Host.Data.Entities;

namespace Catalog.Host.Data.EntityConfig
{
    public class CharacteristicEntityConfig : IEntityTypeConfiguration<CharacteristicEntity>
    {
        public void Configure(EntityTypeBuilder<CharacteristicEntity> builder)
        {
            builder.ToTable("Characteristic");
            builder.HasKey(k => k.Id);

            builder.Property(p => p.Id).UseHiLo("characteristic_hilo").IsRequired();
        }
    }
}
