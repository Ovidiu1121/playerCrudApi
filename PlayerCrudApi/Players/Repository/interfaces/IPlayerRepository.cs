using PlayerCrudApi.Dto;
using PlayerCrudApi.Players.Model;

namespace PlayerCrudApi.Players.Repository.interfaces
{
    public interface IPlayerRepository
    {
        Task<ListPlayerDto> GetAllAsync();
        Task<PlayerDto> GetByNameAsync(string name);
        Task<PlayerDto> GetByIdAsync(int id);
        Task<PlayerDto> CreatePlayer(CreatePlayerRequest request);
        Task<PlayerDto> UpdatePlayer(int id, UpdatePlayerRequest request);
        Task<PlayerDto> DeletePlayerById(int id);
    }
}
