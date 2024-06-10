using PlayerCrudApi.Players.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tests.Helpers
{
    public class TestPlayerFactory
    {
        public static Player CreatePlayer(int id)
        {
            return new Player
            {
                Id = id,
                Name ="Spiderman"+id,
                Number=5+id,
                Value=10000+id
            };

        }

        public static List<Player> CreatePlayers(int count)
        {

            List<Player> players = new List<Player>();

            for (int i = 0; i<count; i++)
            {
                players.Add(CreatePlayer(i));
            }

            return players;
        }
    }
}
