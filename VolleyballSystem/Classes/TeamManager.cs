using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolleyballSystem.Interfaces;

namespace VolleyballSystem.Classes
{
    public class TeamManager
    {
        public List<Team> ListTeams { get; set; } = new List<Team>();
        public List<Player> Players = new List<Player>();

        public TeamManager(ITeamRepository teamRepository, IPlayerRepository playerRepository)
        {
            ListTeams = teamRepository.GetAllTeams().ToList();
            Players = playerRepository.GetAllPlayers().ToList();
            
            foreach (Team team in ListTeams)
            {
                team.Players = Players.Where(player => player.TeamID == team.Id).ToList();
            }
        }

        public List<Team> GetTeams()
        {
            return ListTeams;
        }
    }
}
