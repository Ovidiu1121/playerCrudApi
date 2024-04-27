using PlayerCrudApi.Dto;
using PlayerCrudApi.Players.Model;

namespace PlayerCrudApi.Players.Repository.interfaces
{
    public interface IPlayerRepository
    {
        Task<IEnumerable<Player>> GetAllAsync();
        Task<Player> GetByNameAsync(string name);
        Task<Player> GetByIdAsync(int id);
        Task<Player> CreatePlayer(CreatePlayerRequest request);
        Task<Player> UpdatePlayer(int id, UpdatePlayerRequest request);
        Task<Player> DeletePlayerById(int id);
    }
}
