using PlayerCrudApi.Players.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlayerCrudApi.Dto;

namespace tests.Helpers
{
    public class TestPlayerFactory
    {
        public static PlayerDto CreatePlayer(int id)
        {
            return new PlayerDto
            {
                Id = id,
                Name ="Spiderman"+id,
                Number=5+id,
                Value=10000+id
            };

        }

        public static ListPlayerDto CreatePlayers(int count)
        {

            ListPlayerDto players = new ListPlayerDto();

            for (int i = 0; i<count; i++)
            {
                players.playerList.Add(CreatePlayer(i));
            }

            return players;
        }
    }
}
