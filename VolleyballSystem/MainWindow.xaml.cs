using System.ComponentModel;
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
using VolleyballSystem.Interfaces;
using VolleyballSystem.Pages;
using VolleyballSystem.Services;
using static VolleyballSystem.Interfaces.IPlayerRepository;

namespace VolleyballSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Team> teams;
        private StandingsManager _standingsManager = new StandingsManager(new MockTeamRepository(), new MockPlayerRepository());

        public MainWindow()
        {
            /*
            DatabaseHelper.InitializeDatabase();
            DatabaseHelper.AddDataFromCsv("teams", @"..\..\..\Files\teams.csv");
            DatabaseHelper.AddDataFromCsv("players", @"..\..\..\Files\players.csv");
            */
            InitializeComponent();
            Loaded += (sender, e) => SetFullScreen();

            /*
            SQLiteTeams sqliteTeams = new SQLiteTeams();
            IEnumerable<Team> teams = sqliteTeams.GetAllTeams();
            */
            //DataContext = new MainViewModel();

            teams = _standingsManager.teams;

            teams[0].Players = new List<Player>()
            {
                new Player("Karol", "Musza", "Setter"),
                new Player("Karol", "Dusza", "Libero"),
                new Player("Jakub", "Musza", "Outisde Hitter"),
                new Player("Marcin", "Musza", "Opposite Hitter")
            };

            teams[1].Players = new List<Player>()
            {
                new Player("Mateusz", "Kusza", "Setter"),
                new Player("Karol", "Dusza", "Libero"),
                new Player("Henryk", "Musza", "Outisde Hitter"),
            };

            _standingsManager.ListStandings = new List<Standings>
            {
                new Standings(teams[0],5,15,15,0),
                new Standings(teams[1],5,12,3,21),
                new Standings(teams[2],5,10,2,1),
                new Standings(teams[3],5,2,3,56),
                new Standings(teams[4],5,2,3,12)
            };
            List<Standings> standings = _standingsManager.ListStandings;

            // clear listView and comboboxes
            comboHost.Items.Clear();
            comboGuest.Items.Clear();
            listViewTeams.Items.Clear();

            comboHost.ItemsSource = teams;
            comboHost.DisplayMemberPath = "TeamName";
            comboGuest.ItemsSource = teams;
            comboGuest.DisplayMemberPath = "TeamName";

            listViewTeams.ItemsSource = standings;
        }

        private void BtnAddPlayer(object sender, RoutedEventArgs e)
        {
            /*
            if (listViewTeams.SelectedItem != null)
            {
                Standings selectedStanding = listViewTeams.SelectedItem as Standings;
                Team selectedTeam = selectedStanding.Team;
                AddPlayer addPlayer = new AddPlayer(selectedTeam);
                addPlayer.DataContext = DataContext;
                //NavigationFrame.NavigationService.Navigate(addPlayer);
            }
            else
            {
                MessageBox.Show("Select Team");
            }
            */
        }

        private void BtnDeletePlayer(object sender, RoutedEventArgs e)
        {
            if (listViewPlayers.SelectedItem != null)
            {
                int indexPlayer = listViewPlayers.SelectedIndex;
                Team team = listViewTeams.SelectedItem as Team;
                team.Players.RemoveAt(indexPlayer);

                Player player = listViewPlayers.SelectedItem as Player;
                //team.Players.Remove(player);
                MessageBox.Show("Player has been deleted");
                MessageBox.Show(indexPlayer.ToString());
            }
            else
            {
                MessageBox.Show("Select Player");
            }
        }

        private void BtnAddMatch(object sender, RoutedEventArgs e)
        {
            if (comboHost.SelectedItem != comboGuest.SelectedItem)
            {
                MessageBox.Show(comboHost.SelectedIndex.ToString());
            }
            else if (textBoxScoreGuest.Text.Equals("") || textBoxScoreHost.Text.Equals(""))
            {
                MessageBox.Show("Input Score");
            }
            else if (Int32.Parse(textBoxScoreGuest.Text) > 3 || Int32.Parse(textBoxScoreHost.Text) > 3)
            {
                MessageBox.Show("Sets can't be greater than 3");
            }
            else if (comboHost.SelectedItem == null || comboGuest.SelectedItem == null)
            {
                MessageBox.Show("Select Teams");
            }
            else
            {
                MessageBox.Show("Teams are the same! Change it!");
                return;
            }
        }

        private void TeamsSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //listViewPlayers.Items.Clear();

            foreach (Standings standings in listViewTeams.Items)
            {
                Standings selectedStanding = listViewTeams.SelectedItem as Standings;
                Team selectedTeam = selectedStanding.Team;
                listViewPlayers.ItemsSource = selectedTeam.Players;
            }
            /*
            if (listViewTeams.SelectedItem != null)
            {
                Standings selectedStanding = listViewTeams.SelectedItem as Standings;
                Team selectedTeam = selectedStanding.Team;
                listViewPlayers.ItemsSource = selectedTeam.Players;
            }
            */
        }

        private void textBoxScore_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!IsNumeric(e.Text))
            {
                e.Handled = true;
            }

            if ((sender as TextBox)?.Text.Length >= 1)
            {
                e.Handled = true;
                return;
            }
        }

        private bool IsNumeric(string text)
        {
            return int.TryParse(text, out _);
        }

        private void SetFullScreen()
        {
            WindowState = WindowState.Maximized;
        }
    }
}