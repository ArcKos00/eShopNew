using Catalog.Host.Data.Entities;

namespace Catalog.Host.Data.EntityConfig
{
    public class ArtefactEntityConfig : IEntityTypeConfiguration<ArtefactEntity>
    {
        public void Configure(EntityTypeBuilder<ArtefactEntity> builder)
        {
            builder.ToTable("Artefact");

            builder.Property(p => p.Id).UseHiLo("artefact_hilo").IsRequired();
            builder.Property(p => p.Name).IsRequired().HasMaxLength(50);
            builder.Property(p => p.Nature).IsRequired().HasMaxLength(50);
            builder.Property(p => p.ImagePath).IsRequired();

            builder.HasOne(o => o.Anomaly)
                .WithMany()
                .HasForeignKey(k => k.AnomalyId);
            builder.HasOne(o => o.Type)
                .WithMany()
                .HasForeignKey(k => k.AbnormalTypeId);
            builder.HasOne(o => o.Meets)
                .WithMany()
                .HasForeignKey(k => k.FrequencyId);
            builder.HasOne(o => o.Values)
                .WithMany()
                .HasForeignKey(k => k.CharacteristicId);
        }
    }
}
