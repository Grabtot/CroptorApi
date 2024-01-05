using Croptor.Api.ViewModels.Size;
using Croptor.Domain.Common.ValueObjects;
using Mapster;

namespace Croptor.Api.Common.Mapping
{
    public class PresetMappingConfigurations : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<AddSizeDto, Size>()
                .Map(dest => dest, src => new Size(src.Width, src.Height, null, null));
        }
    }
}
