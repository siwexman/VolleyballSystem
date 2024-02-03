using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolleyballSystem.Classes
{
    public class Player
    {
        private int Id { get; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
        public int TeamID { get; set; }

        public Player(string firstName, string lastName, string position)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Position = position;
        }

        public Player(string firstName, string lastName, string position, int teamID)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Position = position;
            this.TeamID = teamID;
        }

        public string ShowPlayer()
        {
            return $"{this.FirstName}, {this.LastName}, {this.Position}";
        }
    }
}
