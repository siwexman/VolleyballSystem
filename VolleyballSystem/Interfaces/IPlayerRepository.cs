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
    public interface IPlayerRepository
    {
        IEnumerable<Player> GetAllPlayers();
    }

    public class MockPlayerRepository : IPlayerRepository
    {
        public IEnumerable<Player> GetAllPlayers()
        {
            return new List<Player>()
                {
                    new Player("Mateusz", "Kusza", "Setter"),
                    new Player("Karol", "Dusza", "Libero"),
                    new Player("Henryk", "Musza", "Outisde Hitter"),
                    new Player("Karol", "Musza", "Setter"),
                    new Player("Karol", "Dusza", "Libero"),
                    new Player("Jakub", "Musza", "Outisde Hitter"),
                    new Player("Marcin", "Musza", "Opposite Hitter")
                };
        }

    }

    public class SQLitePlayers : IPlayerRepository
    {
        private string connectionString = @"Data Source=..\..\..\Files\VolleyballSystem.db;Version=3";

        public IEnumerable<Player> GetAllPlayers()
        {
            List<Player> players = new List<Player>();

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM PLAYERS";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(reader.GetOrdinal("PlayerID"));
                            string firstName = reader.GetString(reader.GetOrdinal("FirstName"));
                            string lastName = reader.GetString(reader.GetOrdinal("LastName"));
                            string position = reader.GetString(reader.GetOrdinal("Position"));
                            int teamid = reader.GetInt32(reader.GetOrdinal("TeamID"));

                            players.Add(new Player(firstName, lastName, position, teamid));
                        }
                    }
                }
            }

            return players;
        }

        public static void AddPlayer(string connectionString, Player player)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string insertPlayerQuery = @"
                                            INSERT INTO PLAYERS (FirstName, LastName, Position, TeamID)
                                            VALUES (@FirstName, @LastName, @Position, @TeamID)
                                            ";

                using (SQLiteCommand command = new SQLiteCommand(insertPlayerQuery, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", player.FirstName);
                    command.Parameters.AddWithValue("@LastName", player.LastName);
                    command.Parameters.AddWithValue("@Position", player.Position);
                    command.Parameters.AddWithValue("@TeamID", player.TeamID);

                    command.ExecuteNonQuery();
                }

            }
        }

        public static void RemovePlayer(string connectionString, Player player)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string insertPlayerQuery = @"
                                            DELETE FROM PLAYERS
                                            WHERE FirstName = @FirstName and LastName = @LastName
                                            ";

                using (SQLiteCommand command = new SQLiteCommand(insertPlayerQuery, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", player.FirstName.ToString());
                    command.Parameters.AddWithValue("@LastName", player.LastName.ToString());

                    command.ExecuteNonQuery();
                }
            }
        }
    }

}
