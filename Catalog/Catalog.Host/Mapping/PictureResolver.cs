using Catalog.Host.Configurations;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;

namespace Catalog.Host.Mapping
{
    public class PictureResolver : IMemberValueResolver<ArtefactEntity, Artefact, string, object>
    {
        private readonly Config _config;
        public PictureResolver(IOptionsSnapshot<Config> config)
        {
            _config = config.Value;
        }

        public object Resolve(ArtefactEntity source, Artefact destination, string sourceMember, object destMember, ResolutionContext context)
        {
            return $"{_config.CdnHost}/{_config.ImgUrl}/{sourceMember}";
        }
    }
}
