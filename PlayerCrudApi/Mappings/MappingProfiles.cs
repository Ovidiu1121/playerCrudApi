using AutoMapper;
using PlayerCrudApi.Dto;
using PlayerCrudApi.Players.Model;

namespace PlayerCrudApi.Mappings
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<CreatePlayerRequest, Player>();
            CreateMap<UpdatePlayerRequest, Player>();
            CreateMap<PlayerDto, Player>().ReverseMap();
        }
    }
}
