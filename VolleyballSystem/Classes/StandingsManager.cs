using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolleyballSystem.Interfaces;

namespace VolleyballSystem.Classes
{
    public class StandingsManager
    {
        public List<Standings> ListStandings { get; set; } = new List<Standings>();
        public List<Team> teams;
        public List<Player> players;

        public StandingsManager(ITeamRepository teamRepository, IPlayerRepository playerRepository)
        {
            teams = teamRepository.GetAllTeams().ToList<Team>();
            players = playerRepository.GetAllPlayers().ToList<Player>();
        }

        public void AddStats(Match match)
        {
            Standings standingsHost = new Standings(match.HostTeam);
            Standings standingsGuest = new Standings(match.GuestTeam);

            // Incrementing MatchesPlayed
            standingsHost.MatchesPlayed++;
            standingsGuest.MatchesPlayed++;

            // Adding Score to Host and Guest Team
            standingsHost.SetsWon += match.ScoreHost;
            standingsHost.SetsLose += match.ScoreGuest;

            standingsHost.SetsWon += match.ScoreHost;
            standingsHost.SetsLose += match.ScoreGuest;

            if (match.ScoreHost > match.ScoreGuest) // who scored more
            {
                // true - host win
                if (match.ScoreHost - match.ScoreGuest > 1)
                {
                    standingsHost.Points += 3;
                }
                else
                {
                    // tie-break
                    standingsHost.Points += 2;
                    standingsGuest.Points += 1;
                }
            }
            else
            {
                // false - guest win
                if (match.ScoreGuest - match.ScoreHost > 1)
                {
                    standingsGuest.Points += 3;
                }
                else
                {
                    // tie-break
                    standingsGuest.Points += 2;
                    standingsHost.Points += 1;
                }
            }
        }
    }
}
