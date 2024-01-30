using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolleyballSystem.Interfaces
{
    interface IPlayers
    {
        IEnumerable<IPlayers> GetAllPlayers();
    }
}
