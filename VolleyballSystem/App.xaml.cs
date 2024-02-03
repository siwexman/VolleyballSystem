using System.Configuration;
using System.Data;
using System.Windows;
using VolleyballSystem.Services;

namespace VolleyballSystem
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            // Database inicialization
            DatabaseHelper.InitializeDatabase();

            if (!DatabaseHelper.DataHasBeenAdded()) // checks if file already exists
            {
                DatabaseHelper.AddDataFromCsv("teams", @"..\..\..\Files\teams.csv");
                DatabaseHelper.AddDataFromCsv("players", @"..\..\..\Files\players.csv");
                DatabaseHelper.SetDataAddedFlag();
            }
        }
    }

}
