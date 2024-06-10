using PlayerCrudApi.Dto;
using PlayerCrudApi.Players.Model;
using PlayerCrudApi.Players.Repository.interfaces;
using PlayerCrudApi.Players.Service.interfaces;
using PlayerCrudApi.System.Constant;
using PlayerCrudApi.System.Exceptions;

namespace PlayerCrudApi.Players.Service
{
    public class PlayerCommandService: IPlayerCommandService
    {
        private IPlayerRepository _repository;

        public PlayerCommandService(IPlayerRepository repository)
        {
            _repository = repository;
        }

        public async Task<Player> CreatePlayer(CreatePlayerRequest request)
        {
            Player player = await _repository.GetByNameAsync(request.Name);

            if (player!=null)
            {
                throw new ItemAlreadyExists(Constants.PLAYER_ALREADY_EXIST);
            }

            player=await _repository.CreatePlayer(request);
            return player;
        }

        public async Task<Player> DeletePlayer(int id)
        {
            Player player = await _repository.GetByIdAsync(id);

            if (player==null)
            {
                throw new ItemDoesNotExist(Constants.PLAYER_DOES_NOT_EXIST);
            }

            await _repository.DeletePlayerById(id);
            return player;
        }

        public async Task<Player> UpdatePlayer(int id, UpdatePlayerRequest request)
        {

            Player player = await _repository.GetByIdAsync(id);

            if (player==null)
            {
                throw new ItemDoesNotExist(Constants.PLAYER_DOES_NOT_EXIST);
            }

            player = await _repository.UpdatePlayer(id, request);
            return player;
        }
    }
}
