using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolleyballSystem.Classes
{
    public class Standing
    {
        public int MatchesPlayed { get; set; }
        public int Points { get; set; }
        public int SetsWon { get; set; }
        public int SetsLose { get; set; }
        public Team Team { get; set; }

        public Standing(Team team)
        {
            this.Team = team;
        }

        /*
        public Standing(Team team, int matchesPlayed, int points, int setsWon, int setsLose)
        {
            this.Team = team;
            this.MatchesPlayed = matchesPlayed;
            this.Points = points;
            this.SetsWon = setsWon;
            this.SetsLose = setsLose;
        }
        */

        public Team GetTeam() { return Team; }
    }
}
