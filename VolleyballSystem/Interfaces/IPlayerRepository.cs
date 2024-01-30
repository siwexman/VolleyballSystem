using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolleyballSystem.Classes;

namespace VolleyballSystem.Interfaces
{
    public interface IPlayerRepository
    {
        IEnumerable<Player> GetAllPlayers();
        IEnumerable<Player> GetAllPlayersOfTeam(Team Team);

        public class MockPlayerRepository : IPlayerRepository
        {
            public IEnumerable<Player> GetAllPlayers()
            {
                return new List<Player>()
                {
                    new Player("Mateusz", "Kusza", "Setter"),
                    new Player("Karol", "Dusza", "Libero"),
                    new Player("Henryk", "Musza", "Outisde Hitter"),
                    new Player("Karol", "Musza", "Setter"),
                    new Player("Karol", "Dusza", "Libero"),
                    new Player("Jakub", "Musza", "Outisde Hitter"),
                    new Player("Marcin", "Musza", "Opposite Hitter")
                };
            }

            public IEnumerable<Player> GetAllPlayersOfTeam(Team team)
            {
                return team.Players = new List<Player>()
                {
                    new Player("Karol", "Musza", "Setter"),
                    new Player("Karol", "Dusza", "Libero"),
                    new Player("Jakub", "Musza", "Outisde Hitter"),
                    new Player("Marcin", "Musza", "Opposite Hitter")
                };
            }
        }
    }
}
