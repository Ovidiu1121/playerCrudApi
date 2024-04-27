using PlayerCrudApi.Players.Model;
using PlayerCrudApi.Players.Repository.interfaces;
using PlayerCrudApi.Players.Service.interfaces;
using PlayerCrudApi.System.Constant;
using PlayerCrudApi.System.Exceptions;

namespace PlayerCrudApi.Players.Service
{
    public class PlayerQueryService: IPlayerQueryService
    {
        private IPlayerRepository _repository;

        public PlayerQueryService(IPlayerRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Player>> GetAllPlayers()
        {
            IEnumerable<Player> players = await _repository.GetAllAsync();

            if (players.Count().Equals(0))
            {
                throw new ItemDoesNotExist(Constants.NO_PLAYERS_EXIST);
            }

            return players;
        }

        public async Task<Player> GetById(int id)
        {
            Player players = await _repository.GetByIdAsync(id);

            if (players == null)
            {
                throw new ItemDoesNotExist(Constants.PLAYER_DOES_NOT_EXIST);
            }

            return players;
        }

        public async Task<Player> GetByName(string name)
        {

            Player players = await _repository.GetByNameAsync(name);

            if (players == null)
            {
                throw new ItemDoesNotExist(Constants.PLAYER_DOES_NOT_EXIST);
            }

            return players;
        }
    }
}
