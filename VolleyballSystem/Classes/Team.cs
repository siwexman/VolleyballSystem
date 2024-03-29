﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolleyballSystem.Classes
{
    public class Team
    {
        public int Id { get; }
        public string TeamName { get; set; }
        public List<Player> Players { get; set; }

        public Team(string teamName)
        {
            this.TeamName = teamName;
        }

        public Team(int id, string teamName)
        {
            this.Id = id;
            this.TeamName = teamName;
        }
    }
}
