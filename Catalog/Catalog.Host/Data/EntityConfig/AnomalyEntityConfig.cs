using Catalog.Host.Data.Entities;

namespace Catalog.Host.Data.EntityConfig
{
    public class AnomalyEntityConfig : IEntityTypeConfiguration<AnomalyEntity>
    {
        public void Configure(EntityTypeBuilder<AnomalyEntity> builder)
        {
            builder.ToTable("Anomaly");
            builder.HasKey(x => x.Id);

            builder.Property(p => p.Id).UseHiLo("anomaly_hilo").IsRequired();
            builder.Property(p => p.Name).IsRequired().HasMaxLength(50);

            builder.HasOne(o => o.Type)
                .WithMany()
                .HasForeignKey(k => k.AbnormalTypeId);
            builder.HasOne(o => o.LocationPlace)
                .WithMany()
                .HasForeignKey(k => k.LocationId);
            builder.HasOne(o => o.Meets)
                .WithMany()
                .HasForeignKey(k => k.FrequencyId);
        }
    }
}
