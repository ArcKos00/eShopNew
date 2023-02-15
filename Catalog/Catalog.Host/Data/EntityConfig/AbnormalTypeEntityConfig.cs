using Catalog.Host.Data.Entities;

namespace Catalog.Host.Data.EntityConfig
{
    public class AbnormalTypeEntityConfig : IEntityTypeConfiguration<AbnormalTypeEntity>
    {
        public void Configure(EntityTypeBuilder<AbnormalTypeEntity> builder)
        {
            builder.ToTable("AbnormalType");
            builder.HasKey(k => k.Id);

            builder.Property(p => p.Id).UseHiLo("abnormal_type_hilo").IsRequired();
            builder.Property(p => p.Name).IsRequired().HasMaxLength(50);
        }
    }
}
