using PlayerCrudApi.Dto;
using PlayerCrudApi.Players.Model;

namespace PlayerCrudApi.Players.Service.interfaces
{
    public interface IPlayerCommandService
    {
        Task<Player> CreatePlayer(CreatePlayerRequest request);
        Task<Player> UpdatePlayer(int id, UpdatePlayerRequest request);
        Task<Player> DeletePlayer(int id);
    }
}
