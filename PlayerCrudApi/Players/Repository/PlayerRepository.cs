using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PlayerCrudApi.Data;
using PlayerCrudApi.Dto;
using PlayerCrudApi.Players.Model;
using PlayerCrudApi.Players.Repository.interfaces;

namespace PlayerCrudApi.Players.Repository
{
    public class PlayerRepository: IPlayerRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public PlayerRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Player> CreatePlayer(CreatePlayerRequest request)
        {
            var player = _mapper.Map<Player>(request);

            _context.Players.Add(player);

            await _context.SaveChangesAsync();

            return player;
        }

        public async Task<Player> DeletePlayerById(int id)
        {
            var player = await _context.Players.FindAsync(id);

            _context.Players.Remove(player);

            await _context.SaveChangesAsync();

            return player;
        }

        public async Task<IEnumerable<Player>> GetAllAsync()
        {
            return await _context.Players.ToListAsync();
        }

        public async Task<Player> GetByIdAsync(int id)
        {
            return await _context.Players.FirstOrDefaultAsync(obj => obj.Id.Equals(id));
        }

        public async Task<Player> GetByNameAsync(string name)
        {
            return await _context.Players.FirstOrDefaultAsync(obj => obj.Name.Equals(name));
        }

        public async Task<Player> UpdatePlayer(int id, UpdatePlayerRequest request)
        {
            var player = await _context.Players.FindAsync(id);

            player.Name= request.Name ?? player.Name;
            player.Number=request.Number ?? player.Number;
            player.Value=request.Value ?? player.Value;

            _context.Players.Update(player);

            await _context.SaveChangesAsync();

            return player;
        }
    }
}
