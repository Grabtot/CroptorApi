using Croptor.Application.Orders.Queries.CreateWayForPay.User;
using Croptor.Domain.Users;
using Mapster;

namespace Croptor.Api.Common.Mapping
{
    public class UserMappingConfigurations : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<User, UserDto>()
                .Map(dest => dest.Name, src => src.UserName)
                .Map(dest => dest.Plan, src => src.Plan.Type.ToString())
                .Map(dest => dest.Expires, src => src.Plan.ExpireDate);
        }
    }
}
