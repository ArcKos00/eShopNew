using Catalog.Host.Data.Entities;

namespace Catalog.Host.Data.EntityConfig
{
    public class FrequencyEntityConfig : IEntityTypeConfiguration<FrequencyEntity>
    {
        public void Configure(EntityTypeBuilder<FrequencyEntity> builder)
        {
            builder.ToTable("Meets");
            builder.HasKey(k => k.Id);

            builder.Property(p => p.Id).UseHiLo("frequency_hilo").IsRequired();
            builder.Property(p => p.Meets).IsRequired();
        }
    }
}
