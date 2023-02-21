using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;

namespace Catalog.Host.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ArtefactEntity, Artefact>()
                .ForMember("ImageUrl", option => option.MapFrom<PictureResolver, string>(r => r.ImagePath));
            CreateMap<AbnormalTypeEntity, AbnormalType>();
            CreateMap<AnomalyEntity, Anomaly>();
            CreateMap<FrequencyEntity, Frequency>();
            CreateMap<LocationEntity, Location>();
            CreateMap<CharacteristicEntity, Characteristic>();
        }
    }
}
