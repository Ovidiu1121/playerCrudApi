using PlayerCrudApi.Dto;
using PlayerCrudApi.Players.Model;

namespace PlayerCrudApi.Players.Service.interfaces
{
    public interface IPlayerQueryService
    {
        Task<ListPlayerDto> GetAllPlayers();
        Task<PlayerDto> GetByName(string name);
        Task<PlayerDto> GetById(int id);
    }
}
