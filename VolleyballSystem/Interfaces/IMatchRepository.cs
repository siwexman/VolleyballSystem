using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolleyballSystem.Classes;

namespace VolleyballSystem.Interfaces
{
    public interface IMatchRepository
    {
        IEnumerable<Match> GetAllMatches();
    }

    public class MockMatchRepository : IMatchRepository
    {
        public IEnumerable<Match> GetAllMatches()
        {
            return new List<Match>()
            {
                new Match(1, 2, 3, 1),
                new Match(2, 3, 3, 2),
                new Match(4, 5, 1, 3)
            };
        }
    }

    public class SQLiteMatch : IMatchRepository
    {
        private string connectionString = @"Data Source=..\..\..\..\Files\VolleyballSystem.db;Version=3";

        public IEnumerable<Match> GetAllMatches()
        {
            List<Match> matches = new List<Match>();

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM MATCHES";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(reader.GetOrdinal("MatchID"));
                            int hostTeamID = reader.GetInt32(reader.GetOrdinal("HostTeamID"));
                            int guestTeamID = reader.GetInt32(reader.GetOrdinal("GuestTeamID"));
                            int scoreHost = reader.GetInt32(reader.GetOrdinal("ScoreHost"));
                            int scoreGuest = reader.GetInt32(reader.GetOrdinal("ScoreGuest"));

                            matches.Add(new Match(hostTeamID, guestTeamID, scoreHost, scoreGuest));
                        }
                    }
                }
            }

            return matches;
        }

        public static void AddMatch(string connectionString, Match match)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string insertMatchQuery = @"
                                            INSERT INTO MATCHES (HostTeamID, GuestTeamID, ScoreHost, ScoreGuest)
                                            VALUES (@HostTeamID, @GuestTeamID, @ScoreHost, @ScoreGuest)
                                            ";

                using (SQLiteCommand command = new SQLiteCommand(insertMatchQuery, connection))
                {
                    command.Parameters.AddWithValue("@HostTeamID", match.HostTeamID);
                    command.Parameters.AddWithValue("@GuestTeamID", match.GuestTeamID);
                    command.Parameters.AddWithValue("@ScoreHost", match.ScoreHost);
                    command.Parameters.AddWithValue("@ScoreGuest", match.ScoreGuest);

                    command.ExecuteNonQuery();
                }

            }
        }
    }

}
