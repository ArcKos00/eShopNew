using Catalog.Host.Data.Entities;
using Microsoft.IdentityModel.Tokens;

namespace Catalog.Host.Data
{
    public static class DbInitializer
    {
        public static async Task Initialize(ApplicationDbContext context)
        {
            await context.Database.EnsureCreatedAsync();

            if (!context.Location.Any())
            {
                await context.Location.AddRangeAsync(GetPreConfigLocations());
                await context.SaveChangesAsync();
            }

            if (!context.Frequency.Any())
            {
                await context.Frequency.AddRangeAsync(GetPreConfigMeets());
                await context.SaveChangesAsync();
            }

            if (!context.Characteristic.Any())
            {
                await context.Characteristic.AddRangeAsync(GetPreConfigCharacteristics());
                await context.SaveChangesAsync();
            }

            if (!context.AbnormalType.Any())
            {
                await context.AbnormalType.AddRangeAsync(GetPreConfigTypes());
                await context.SaveChangesAsync();
            }

            if (!context.Anomaly.Any())
            {
                await context.Anomaly.AddRangeAsync(GetPreConfigAnomalies());
                await context.SaveChangesAsync();
            }

            if (!context.Artefact.Any())
            {
                await context.Artefact.AddRangeAsync(GetPreConfigArtefacts());
                await context.SaveChangesAsync();
            }
        }

        public static List<AbnormalTypeEntity> GetPreConfigTypes()
        {
            return new List<AbnormalTypeEntity>()
            {
                new AbnormalTypeEntity() { Name = "Electro" },
                new AbnormalTypeEntity() { Name = "Fire" },
                new AbnormalTypeEntity() { Name = "Gravi" },
                new AbnormalTypeEntity() { Name = "Chemical" },
                new AbnormalTypeEntity() { Name = "Place" }
            };
        }

        public static List<AnomalyEntity> GetPreConfigAnomalies()
        {
            return new List<AnomalyEntity>()
            {
                 new AnomalyEntity() { Name = "Elektra", AbnormalTypeId = 1, LocationId = 1, FrequencyId = 3 },
                 new AnomalyEntity() { Name = "Voronka", AbnormalTypeId = 3, LocationId = 1, FrequencyId = 3 },
                 new AnomalyEntity() { Name = "Fire Stolb", AbnormalTypeId = 2, LocationId = 2, FrequencyId = 2 },
                 new AnomalyEntity() { Name = "Kisel'", AbnormalTypeId = 4, LocationId = 3, FrequencyId = 4 },
                 new AnomalyEntity() { Name = "Oasis", AbnormalTypeId = 5, LocationId = 2, FrequencyId = 5 },
                 new AnomalyEntity() { Name = "Radioactive Release", AbnormalTypeId = 5, LocationId = 4, FrequencyId = 3 }
            };
        }

        public static List<ArtefactEntity> GetPreConfigArtefacts()
        {
            return new List<ArtefactEntity>()
            {
                new ArtefactEntity()
                {
                    Name = "Battery",
                    Nature = "Electro-Gravi",
                    Cost = 6000,
                    ImagePath = "1.png",
                    AbnormalTypeId = 1,
                    AnomalyId = 1,
                    CharacteristicId = 1,
                    FrequencyId = 3
                },
                new ArtefactEntity()
                {
                    Name = "Pacifier",
                    Nature = "Electro-Gravi",
                    Cost = 12000,
                    ImagePath = "2.png",
                    AbnormalTypeId = 1,
                    AnomalyId = 1,
                    CharacteristicId = 2,
                    FrequencyId = 2
                },
                new ArtefactEntity()
                {
                    Name = "Snowflake",
                    Nature = "Electro-Gravi",
                    Cost = 18000,
                    ImagePath = "3.png",
                    AbnormalTypeId = 1,
                    AnomalyId = 1,
                    CharacteristicId = 3,
                    FrequencyId = 1
                },
                new ArtefactEntity()
                {
                    Name = "Soul",
                    Nature = "Chemical",
                    Cost = 6000,
                    ImagePath = "4.png",
                    AbnormalTypeId = 4,
                    AnomalyId = 4,
                    CharacteristicId = 4,
                    FrequencyId = 3
                },
                new ArtefactEntity()
                {
                    Name = "Dumpling",
                    Nature = "Chemical",
                    Cost = 12000,
                    ImagePath = "5.png",
                    AbnormalTypeId = 4,
                    AnomalyId = 4,
                    CharacteristicId = 5,
                    FrequencyId = 2
                },
                new ArtefactEntity()
                {
                    Name = "Firefly",
                    Nature = "Chemical",
                    Cost = 18000,
                    ImagePath = "6.png",
                    AbnormalTypeId = 4,
                    AnomalyId = 4,
                    CharacteristicId = 6,
                    FrequencyId = 1
                },
                new ArtefactEntity()
                {
                    Name = "Mother's Beads",
                    Nature = "Flame",
                    Cost = 6000,
                    ImagePath = "7.png",
                    AbnormalTypeId = 2,
                    AnomalyId = 3,
                    CharacteristicId = 7,
                    FrequencyId = 3
                },
                new ArtefactEntity()
                {
                    Name = "Eye",
                    Nature = "Flame",
                    Cost = 12000,
                    ImagePath = "8.png",
                    AbnormalTypeId = 2,
                    AnomalyId = 3,
                    CharacteristicId = 8,
                    FrequencyId = 2
                },
                new ArtefactEntity()
                {
                    Name = "Flame",
                    Nature = "Flame",
                    Cost = 18000,
                    ImagePath = "9.png",
                    AbnormalTypeId = 2,
                    AnomalyId = 3,
                    CharacteristicId = 9,
                    FrequencyId = 1
                },
                new ArtefactEntity()
                {
                    Name = "Night Star",
                    Nature = "Gravi",
                    Cost = 6000,
                    ImagePath = "10.png",
                    AbnormalTypeId = 3,
                    AnomalyId = 2,
                    CharacteristicId = 10,
                    FrequencyId = 3
                },
                new ArtefactEntity()
                {
                    Name = "Gravi",
                    Nature = "Gravi",
                    Cost = 12000,
                    ImagePath = "11.png",
                    AbnormalTypeId = 3,
                    AnomalyId = 2,
                    CharacteristicId = 11,
                    FrequencyId = 2
                },
                new ArtefactEntity()
                {
                    Name = "Gold Fish",
                    Nature = "Gravi",
                    Cost = 18000,
                    ImagePath = "12.png",
                    AbnormalTypeId = 3,
                    AnomalyId = 2,
                    CharacteristicId = 12,
                    FrequencyId = 1
                },
                new ArtefactEntity()
                {
                    Name = "Jellyfish",
                    Nature = "Gravi",
                    Cost = 4000,
                    ImagePath = "13.png",
                    AbnormalTypeId = 3,
                    AnomalyId = 2,
                    CharacteristicId = 13,
                    FrequencyId = 4
                },
                new ArtefactEntity()
                {
                    Name = "Twist",
                    Nature = "Gravi",
                    Cost = 8000,
                    ImagePath = "14.png",
                    AbnormalTypeId = 3,
                    AnomalyId = 2,
                    CharacteristicId = 14,
                    FrequencyId = 3
                },
                new ArtefactEntity()
                {
                    Name = "Bubble",
                    Nature = "Chemical",
                    Cost = 12000,
                    ImagePath = "15.png",
                    AbnormalTypeId = 4,
                    AnomalyId = 4,
                    CharacteristicId = 15,
                    FrequencyId = 2
                },
                new ArtefactEntity()
                {
                    Name = "Stone Flower",
                    Nature = "Electro",
                    Cost = 3000,
                    ImagePath = "16.png",
                    AbnormalTypeId = 1,
                    AnomalyId = 1,
                    CharacteristicId = 16,
                    FrequencyId = 4
                },
                new ArtefactEntity()
                {
                    Name = "Moon Light",
                    Nature = "Electro",
                    Cost = 6000,
                    ImagePath = "17.png",
                    AbnormalTypeId = 1,
                    AnomalyId = 1,
                    CharacteristicId = 17,
                    FrequencyId = 3
                },
                new ArtefactEntity()
                {
                    Name = "Kristal",
                    Nature = "Flame",
                    Cost = 2000,
                    ImagePath = "18.png",
                    AbnormalTypeId = 2,
                    AnomalyId = 3,
                    CharacteristicId = 18,
                    FrequencyId = 4
                },
                new ArtefactEntity()
                {
                    Name = "Fire Ball",
                    Nature = "Flame",
                    Cost = 4000,
                    ImagePath = "19.png",
                    AbnormalTypeId = 2,
                    AnomalyId = 3,
                    CharacteristicId = 19,
                    FrequencyId = 3
                },
                new ArtefactEntity()
                {
                    Name = "Blood of stone",
                    Nature = "Chemical",
                    Cost = 2000,
                    ImagePath = "20.png",
                    AbnormalTypeId = 4,
                    AnomalyId = 4,
                    CharacteristicId = 20,
                    FrequencyId = 4
                },
                new ArtefactEntity()
                {
                    Name = "A Piece of Meat",
                    Nature = "Chemical",
                    Cost = 4000,
                    ImagePath = "21.png",
                    AbnormalTypeId = 4,
                    AnomalyId = 4,
                    CharacteristicId = 21,
                    FrequencyId = 3
                },
                new ArtefactEntity()
                {
                    Name = "Bengal Fire",
                    Nature = "Electro",
                    Cost = 2000,
                    ImagePath = "22.png",
                    AbnormalTypeId = 1,
                    AnomalyId = 1,
                    CharacteristicId = 22,
                    FrequencyId = 4
                },
                new ArtefactEntity()
                {
                    Name = "Flash",
                    Nature = "Electro",
                    Cost = 4000,
                    ImagePath = "23.png",
                    AbnormalTypeId = 1,
                    AnomalyId = 1,
                    CharacteristicId = 23,
                    FrequencyId = 3
                },
                new ArtefactEntity()
                {
                    Name = "Compass",
                    Nature = "Radioactive Release",
                    ImagePath = "24.png",
                    AbnormalTypeId = 5,
                    AnomalyId = 6,
                    CharacteristicId = 24,
                    FrequencyId = 5
                },
                new ArtefactEntity()
                {
                    Name = "The Heart of the Oasis",
                    Nature = "Oasis",
                    ImagePath = "25.png",
                    AbnormalTypeId = 5,
                    AnomalyId = 1,
                    CharacteristicId = 25,
                    FrequencyId = 5
                }
            };
        }

        public static List<CharacteristicEntity> GetPreConfigCharacteristics()
        {
            return new List<CharacteristicEntity>()
            {
                new CharacteristicEntity()
                {
                    Restoration = 2, Radiation = 1
                },
                new CharacteristicEntity()
                {
                    Restoration = 4, Radiation = 2
                },
                new CharacteristicEntity()
                {
                    Restoration = 6, Radiation = 3
                },
                new CharacteristicEntity()
                {
                    RestorationHealth = 2, Radiation = 2
                },
                new CharacteristicEntity()
                {
                    RestorationHealth = 4, Radiation = 2
                },
                new CharacteristicEntity()
                {
                    RestorationHealth = 6, Radiation = 3
                },
                new CharacteristicEntity()
                {
                    WoundHealing = 2, Radiation = 1
                },
                new CharacteristicEntity()
                {
                    WoundHealing = 4, Radiation = 2
                },
                new CharacteristicEntity()
                {
                    WoundHealing = 6, Radiation = 3
                },
                new CharacteristicEntity()
                {
                    MaximumWeight = 4, Radiation = 1
                },
                new CharacteristicEntity()
                {
                    MaximumWeight = 8, Radiation = 2
                },
                new CharacteristicEntity()
                {
                    MaximumWeight = 12, Radiation = 3
                },
                new CharacteristicEntity()
                {
                    Radiation = -2
                },
                new CharacteristicEntity()
                {
                    Radiation = -3
                },
                new CharacteristicEntity()
                {
                    Radiation = -4
                },
                new CharacteristicEntity()
                {
                    ProtectionDogs = 3, Radiation = 1
                },
                new CharacteristicEntity()
                {
                    ProtectionDogs = 6, Radiation = 2
                },
                new CharacteristicEntity()
                {
                    ThermalProtection = 3, Radiation = 1
                },
                new CharacteristicEntity()
                {
                    ThermalProtection = 6, Radiation = 2
                },
                new CharacteristicEntity()
                {
                    ChemicalProtection = 3, Radiation = 1
                },
                new CharacteristicEntity()
                {
                    ChemicalProtection = 6, Radiation = 2
                },
                new CharacteristicEntity()
                {
                    ElectricalProtection = 3, Radiation = 1
                },
                new CharacteristicEntity()
                {
                    ElectricalProtection = 6, Radiation = 2
                },
                new CharacteristicEntity()
                {
                    Restoration = 2, ProtectionDogs = 3, ElectricalProtection = 3, ChemicalProtection = 3, ThermalProtection = 3, Radiation = 4
                },
                new CharacteristicEntity()
                {
                    Restoration = 2, RestorationHealth = 2, WoundHealing = 2, Saturation = 1
                }
            };
        }

        public static List<FrequencyEntity> GetPreConfigMeets()
        {
            return new List<FrequencyEntity>()
            {
                new FrequencyEntity() { Meets = "Very Rare" },
                new FrequencyEntity() { Meets = "Rare" },
                new FrequencyEntity() { Meets = "Common" },
                new FrequencyEntity() { Meets = "UnCommon" },
                new FrequencyEntity() { Meets = "Special" },
            };
        }

        public static List<LocationEntity> GetPreConfigLocations()
        {
            return new List<LocationEntity>()
            {
                new LocationEntity() { Place = "Zaton" },
                new LocationEntity() { Place = "Upiter" },
                new LocationEntity() { Place = "Pripyat" },
                new LocationEntity() { Place = "Zona" }
            };
        }
    }
}
