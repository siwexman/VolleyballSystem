using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolleyballSystem.Interfaces;

namespace VolleyballSystem.Classes
{
    public class MatchManager
    {
        List<Match> Matches { get; set; } = new List<Match>();

        public MatchManager(IMatchRepository matchRepository)
        {
            Matches = matchRepository.GetAllMatches().ToList();
        }

        public List<Match> GetMeches()
        {
            return Matches;
        }

    }
}
