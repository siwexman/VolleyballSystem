using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolleyballSystem.Classes;
using VolleyballSystem.Interfaces;

namespace VolleyballSystem.Services
{
    public class ManagerHelper
    {
        private TeamManager teamManager = new TeamManager(new SQLiteTeams(), new SQLitePlayers());
        private StandingsManager standingsManager = new StandingsManager();
        private MatchManager matchManager = new MatchManager(new SQLiteMatch());

        public TeamManager TeamManager()
        {
            return teamManager;
        }

        public StandingsManager StandingsManager()
        {
            return standingsManager;
        }
        public MatchManager MatchManager()
        {
            return matchManager;
        }
    }
}

