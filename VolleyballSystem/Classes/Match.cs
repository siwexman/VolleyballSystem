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
        public int HostTeamID { get; set; }
        public int GuestTeamID { get; set; }
        public int ScoreHost { get; set; }
        public int ScoreGuest { get; set; }

        public Match(int hostTeamID, int guestTeamID, int scoreHost, int scoreGuest)
        {
            this.HostTeamID = hostTeamID;
            this.GuestTeamID = guestTeamID;
            this.ScoreHost = scoreHost;
            this.ScoreGuest = scoreGuest;
        }
    }
}
