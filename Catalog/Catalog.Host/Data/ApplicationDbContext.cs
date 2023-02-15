using Catalog.Host.Data.Entities;
using Catalog.Host.Data.EntityConfig;

namespace Catalog.Host.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<CharacteristicEntity> Characteristic { get; set; } = null!;
        public DbSet<AbnormalTypeEntity> AbnormalType { get; set; } = null!;
        public DbSet<FrequencyEntity> Frequency { get; set; } = null!;
        public DbSet<LocationEntity> Location { get; set; } = null!;
        public DbSet<AnomalyEntity> Anomaly { get; set; } = null!;
        public DbSet<ArtefactEntity> Artefact { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new AbnormalTypeEntityConfig());
            builder.ApplyConfiguration(new CharacteristicEntityConfig());
            builder.ApplyConfiguration(new LocationEntityConfig());
            builder.ApplyConfiguration(new FrequencyEntityConfig());
            builder.ApplyConfiguration(new AnomalyEntityConfig());
            builder.ApplyConfiguration(new ArtefactEntityConfig());
        }
    }
}
