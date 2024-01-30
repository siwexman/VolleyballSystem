using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using VolleyballSystem.Classes;

namespace VolleyballSystem.Interfaces
{
    public interface ITeamRepository
    {
        IEnumerable<Team> GetAllTeams();
    }

    public class MockTeamRepository : ITeamRepository
    {
        public IEnumerable<Team> GetAllTeams()
        {
            return new List<Team>()
            {
                new Team("Lubcza"),
                new Team("Urzet"),
                new Team("AKS"),
                new Team("Rakszawa"),
                new Team("Bystrzaki"),
            };
        }
    }

    public class SQLiteTeams : ITeamRepository
    {
        private string connectionString = @"Data Source=..\..\..\Files\VolleyballSystem.db;Version=3";

        public IEnumerable<Team> GetAllTeams()
        {
            List<Team> teams = new List<Team>();

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM TEAMS";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(reader.GetOrdinal("TeamID"));
                            string title = reader.GetString(reader.GetOrdinal("TeamName"));
                        }
                    }
                }
            }

            return teams;
        }
    }
}
