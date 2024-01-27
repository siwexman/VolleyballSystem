using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VolleyballSystem.Classes;
using VolleyballSystem.Pages;
using VolleyballSystem.Services;

namespace VolleyballSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Standings> standings;
        Team team1 = new Team();
        Team team2 = new Team();
        Team team3 = new Team();
        Team team4 = new Team();
        Team team5 = new Team();

        public MainWindow()
        {            
            DatabaseHelper.InitializeDatabase();
            DatabaseHelper.AddDataFromCsv("teams", @"..\..\..\Files\teams.csv");
            DatabaseHelper.AddDataFromCsv("players", @"..\..\..\Files\players.csv");
            
            InitializeComponent();
            DataContext = new MainViewModel();

            team1.TeamName = "Lubcza";
            team2.TeamName = "Urzet";
            team3.TeamName = "AKS";
            team4.TeamName = "Rakszawa";
            team5.TeamName = "Bystrzaki";

            team1.Players = new List<Player>()
            {
                new Player("Karol", "Musza", "Setter"),
                new Player("Karol", "Dusza", "Libero"),
                new Player("Jakub", "Musza", "Outisde Hitter"),
                new Player("Marcin", "Musza", "Opposite Hitter")
            };

            //Content = new ShowSelectedTeam();
            standings = new List<Standings>()
            {
                new Standings(team1,5,15,15,0),
                new Standings(team2,5,12,3,21),
                new Standings(team3,5,10,2,1),
                new Standings(team4,5,2,3,56),
                new Standings(team5,5,2,3,12)
            };
            listViewTeams.Items.Clear();
            listViewTeams.ItemsSource = standings;
        }

        private void BtnAddPlayer(object sender, RoutedEventArgs e)
        {
            if (listViewTeams.SelectedItem != null)
            {
                Standings selectedStanding = listViewTeams.SelectedItem as Standings;
                Team selectedTeam = selectedStanding.Team;
                AddPlayer addPlayer = new AddPlayer(selectedTeam);
                addPlayer.DataContext = DataContext;
                NavigationFrame.NavigationService.Navigate(addPlayer);
            }
            else
            {
                MessageBox.Show("Select Team");
            }
        }

        private void BtnAddMatch(object sender, RoutedEventArgs e)
        {
            AddMatch addMatch = new AddMatch();
            NavigationFrame.NavigationService.Navigate(addMatch);
        }

        private void BtnShowSelectedTeam(object sender, RoutedEventArgs e)
        {
            if (listViewTeams.SelectedItem != null)
            {
                Standings selectedStanding = listViewTeams.SelectedItem as Standings;
                Team selectedTeam = selectedStanding.Team;
                TeamPage teamPage = new TeamPage(selectedTeam);
                teamPage.DataContext = DataContext;
                NavigationFrame.NavigationService.Navigate(teamPage);
            }
        }
    }
}