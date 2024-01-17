using Croptor.Application.Orders.Queries.CreateWayForPay.Preset;
using Croptor.Application.Orders.Queries.CreateWayForPay.Size;
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
                .Map(dest => dest.Id, src => src.Id ?? Guid.NewGuid());
        }
    }
}
