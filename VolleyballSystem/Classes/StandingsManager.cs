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

        public StandingsManager()
        {
            teams = new List<Team>()
            {
                new Team("Lubcza"),
                new Team("Urzet"),
                new Team("AKS"),
                new Team("Rakszawa"),
                new Team("Bystrzaki"),
            };
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
        }
    }
}
