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
            /*
            DatabaseHelper.InitializeDatabase();
            DatabaseHelper.AddDataFromCsv(1, @"..\..\..\Files\teams.csv");
            DatabaseHelper.AddDataFromCsv(0, @"..\..\..\Files\players.csv");
            */
            InitializeComponent();
        }

        private void BtnAddPlayer(object sender, RoutedEventArgs e)
        {
            //Content = new AddPlayer();
            MessageBox.Show("Essa");
        }

        private void BtnAddMatch(object sender, RoutedEventArgs e)
        {
            //Content = new AddMatch();
        }

        private void BtnShowTeams(object sender, RoutedEventArgs e)
        {
            team1.TeamName = "Lubcza";
            team2.TeamName = "Urzet";
            team3.TeamName = "AKS";
            team4.TeamName = "Rakszawa";
            team5.TeamName = "Bystrzaki";
            //Content = new ShowTeams();
            standings = new List<Standings>()
            {
                new Standings(team1,5,15,15,0),
                new Standings(team2,5,12,3,21),
                new Standings(team3,5,10,2,1),
                new Standings(team4,5,2,3,56),
                new Standings(team5,5,2,3,12)
            };

            listViewPlayers.ItemsSource = standings;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (listViewPlayers.SelectedItem != null)
            {
                Standings selectedStanding = listViewPlayers.SelectedItem as Standings;
                standings.Remove(selectedStanding);
                listViewPlayers.Items.Refresh();
            }
        }
    }
}