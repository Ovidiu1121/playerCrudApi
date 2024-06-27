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

        public async Task<PlayerDto> CreatePlayer(CreatePlayerRequest request)
        {
            var player = _mapper.Map<Player>(request);

            _context.Players.Add(player);

            await _context.SaveChangesAsync();

            return _mapper.Map<PlayerDto>(player);
        }

        public async Task<PlayerDto> DeletePlayerById(int id)
        {
            var player = await _context.Players.FindAsync(id);

            _context.Players.Remove(player);

            await _context.SaveChangesAsync();

            return _mapper.Map<PlayerDto>(player);
        }

        public async Task<ListPlayerDto> GetAllAsync()
        {
            List<Player> result = await _context.Players.ToListAsync();
            
            ListPlayerDto listPlayerDto = new ListPlayerDto()
            {
                playerList = _mapper.Map<List<PlayerDto>>(result)
            };

            return listPlayerDto;
        }

        public async Task<PlayerDto> GetByIdAsync(int id)
        {
            var player = await _context.Players.Where(p => p.Id == id).FirstOrDefaultAsync();
            
            return _mapper.Map<PlayerDto>(player);
        }

        public async Task<PlayerDto> GetByNameAsync(string name)
        {
            var player = await _context.Players.Where(p => p.Name.Equals(name)).FirstOrDefaultAsync();
            
            return _mapper.Map<PlayerDto>(player);
        }

        public async Task<PlayerDto> UpdatePlayer(int id, UpdatePlayerRequest request)
        {
            var player = await _context.Players.FindAsync(id);

            player.Name= request.Name ?? player.Name;
            player.Number=request.Number ?? player.Number;
            player.Value=request.Value ?? player.Value;

            _context.Players.Update(player);

            await _context.SaveChangesAsync();

            return _mapper.Map<PlayerDto>(player);
        }
    }
}
