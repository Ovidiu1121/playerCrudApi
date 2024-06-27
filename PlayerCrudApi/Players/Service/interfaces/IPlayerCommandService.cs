using PlayerCrudApi.Dto;
using PlayerCrudApi.Players.Model;

namespace PlayerCrudApi.Players.Service.interfaces
{
    public interface IPlayerCommandService
    {
        Task<PlayerDto> CreatePlayer(CreatePlayerRequest request);
        Task<PlayerDto> UpdatePlayer(int id, UpdatePlayerRequest request);
        Task<PlayerDto> DeletePlayer(int id);
    }
}
