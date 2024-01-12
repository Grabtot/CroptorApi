using Croptor.Api.ViewModels.Preset;
using Croptor.Api.ViewModels.Size;
using Croptor.Domain.Common.ValueObjects;
using Croptor.Domain.Presets;
using Mapster;

namespace Croptor.Api.Common.Mapping
{
    public class PresetMappingConfigurations : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<AddSizeDto, Size>()
                .Map(dest => dest, src => new Size(src.Width, src.Height, null, null));
            config.NewConfig<SavePresetDto, Preset>()
                .Map(dest => dest.Id, src => src.Id ?? Guid.NewGuid());
        }
    }
}
