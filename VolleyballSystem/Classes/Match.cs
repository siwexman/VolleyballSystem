using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolleyballSystem.Classes
{
    public class Match
    {
        public int Id { get; }
        public Team HostTeam { get; set; }
        public Team GuestTeam { get; set; }
        public int ScoreHost { get; set; }
        public int ScoreGuest { get; set; }

        public Match(Team hostTeam, Team guestTeam, int scoreHost, int scoreGuest)
        {
            this.HostTeam = hostTeam;
            this.GuestTeam = guestTeam;
            this.ScoreHost = scoreHost;
            this.ScoreGuest = scoreGuest;
        }
    }
}
