using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolleyballSystem.Classes
{
    internal class Match
    {
        public int Id { get; }
        public Team HostTeamName { get; set; }
        public Team GuestTeamName { get; set; }
        public int ScoreHost { get; set; }
        public int ScoreGuest { get; set; }

        public Match(Team hostTeamName, Team guestTeamName, int scoreHost, int scoreGuest)
        {
            this.HostTeamName = hostTeamName;
            this.GuestTeamName = guestTeamName;
            this.ScoreHost = scoreHost;
            this.ScoreGuest = scoreGuest;
        }
    }
}
