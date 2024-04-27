using PlayerCrudApi.Players.Model;

namespace PlayerCrudApi.Players.Service.interfaces
{
    public interface IPlayerQueryService
    {
        Task<IEnumerable<Player>> GetAllPlayers();
        Task<Player> GetByName(string name);
        Task<Player> GetById(int id);
    }
}
