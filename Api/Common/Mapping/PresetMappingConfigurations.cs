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
            config.NewConfig<SizeDto, Size>()
                .Map(dest => dest, src => new Size(src.Width, src.Height, null, null));

            config.NewConfig<PresetDto, Preset>()
                .Map(dest => dest.Id, src => src.Id ?? Guid.NewGuid())
                .Map(dest => dest.Sizes, src => src.Sizes.ConvertAll(size
                    => new Size(size.Width, size.Height, size.Name, size.IconUri)));

            config.NewConfig<Preset, PresetDto>()
                .MapWith(preset => new(preset.Id, preset.Name, preset.Sizes));
        }
    }
}
