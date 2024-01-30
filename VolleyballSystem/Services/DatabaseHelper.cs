using System;
using System.IO;
using System.Data.SQLite;

namespace VolleyballSystem.Services
{
    public class DatabaseHelper
    {
        private static string connectionString = @"Data Source=..\..\..\Files\VolleyballSystem.db;Version=3";

        public static void InitializeDatabase()
        {
            if (!File.Exists(@"..\..\..\Files\VolleyballSystem.db"))
            {
                SQLiteConnection.CreateFile(@"..\..\..\Files\VolleyballSystem.db");

                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    // Create tables for volleyball data
                    // Teams table
                    string createTeamsTableQuery = @"
                        CREATE TABLE IF NOT EXISTS TEAMS (
                            TeamID INTEGER PRIMARY KEY AUTOINCREMENT,
                            TeamName TEXT NOT NULL
                            );";

                    // Players table
                    string createPlayersTableQuery = @"
                        CREATE TABLE IF NOT EXISTS PLAYERS (
                            PlayerID INTEGER PRIMARY KEY AUTOINCREMENT,
                            FirstName TEXT NOT NULL,
                            LastName TEXT NOT NULL,
                            Position TEXT NOT NULL,
                            TeamID INTEGER,
                            FOREIGN KEY(TeamID) REFERENCES TEAMS(TeamID)
                            );";
                    
                    /*
                    // Matches table
                    string createMatchesTableQuery = @"
                        CREATE TABLE IF NOT EXISTS MATCHES (
                            MatchID INTEGER PRIMARY KEY AUTOINCREMENT,
                            HostTeamID INTEGER,
                            GuestTeamID INTEGER,
                            ScoreHost INTEGER,
                            ScoreGuest INTEGER,
                            RefereeID INTEGER,
                            FOREIGN KEY(HostTeamID) REFERENCES TEAMS(TeamID),
                            FOREIGN KEY(GuestTeamID) REFERENCES TEAMS(TeamID),
                            FOREIGN KEY(RefereeID) REFERENCES REFEREES(RefereeID)
                            );";
                    */
                    using (var command = new SQLiteCommand(connection))
                    {
                        // Executing sql
                        command.CommandText = createTeamsTableQuery;
                        command.ExecuteNonQuery();

                        command.CommandText = createPlayersTableQuery;
                        command.ExecuteNonQuery();

                        //command.CommandText = createRefereesTableQuery;
                        //command.ExecuteNonQuery();

                        //command.CommandText = createMatchesTableQuery;
                        //command.ExecuteNonQuery();
                    }

                }
            }
        }

        public static void AddDataFromCsv(string table, string csvPath)
        {
            if (File.Exists(csvPath))
            {
                string[] lines = File.ReadAllLines(csvPath);

                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    using (SQLiteCommand command = new SQLiteCommand(connection))
                    {
                        bool firstRow = true;

                        foreach (string line in lines)
                        {
                            if (firstRow)
                            {
                                firstRow = false;
                                continue; // Skip the header line
                            }

                            if (table.Equals("players")) // table PLAYERS
                            {
                                string[] values = line.Split(',');
                                string firstName = values[0];
                                string lastName = values[1];
                                string position = values[2];
                                int teamID = Int32.Parse(values[3]);

                                command.CommandText = "INSERT INTO PLAYERS (FirstName, LastName, Position, TeamID) VALUES (@FirstName, @LastName, @Position, @TeamID)";
                                command.Parameters.AddWithValue("@FirstName", firstName);
                                command.Parameters.AddWithValue("@LastName", lastName);
                                command.Parameters.AddWithValue("@Position", position);
                                command.Parameters.AddWithValue("@TeamID", teamID);

                                command.ExecuteNonQuery();
                            }
                            else if (table.Equals("teams")) // table TEAMS
                            {
                                string[] values = line.Split(',');
                                string teamName = values[0];

                                command.CommandText = "INSERT INTO TEAMS (TeamName) VALUES (@TeamName)";
                                command.Parameters.AddWithValue("@TeamName", teamName);

                                command.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
        }

        public static string GetConnectionString()
        {
            return connectionString.Split(';')[0];
        }
    }
}

