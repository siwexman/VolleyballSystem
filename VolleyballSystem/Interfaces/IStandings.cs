using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolleyballSystem.Classes;

namespace VolleyballSystem.Interfaces
{
    public interface IStandings
    {
        public Standings GetStandings(Team team);
    }
}
