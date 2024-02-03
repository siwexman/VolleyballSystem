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
using VolleyballSystem.Services;

namespace VolleyballSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string connectionString = DatabaseHelper.GetConnectionString();
        private TeamManager _teamManager = new TeamManager(new SQLiteTeams(), new SQLitePlayers());
        private StandingsManager _standingsManager = new StandingsManager();
        private MatchManager _matchManager = new MatchManager(new SQLiteMatch());

        private List<Standing> standings = new List<Standing>();
        //ManagerHelper managerHelper = new ManagerHelper();

        public MainWindow()
        {
            //StandingsManager _standingsManager = managerHelper.StandingsManager();
            //TeamManager _teamManager = managerHelper.TeamManager();
            //MatchManager _matchManager = managerHelper.MatchManager();

            _standingsManager.FIllWithTeams(_teamManager.GetTeams()); // fill standings with teams

            InitializeComponent();
            Loaded += (sender, e) => SetFullScreen(); // set window to fullsize

            standings = _standingsManager.ListStandings;
            List<Team> teams = _teamManager.ListTeams;

            // clear listView and comboboxes
            comboHost.Items.Clear();
            comboGuest.Items.Clear();
            listViewTeams.Items.Clear();

            comboHost.ItemsSource = teams;
            comboHost.DisplayMemberPath = "TeamName";
            comboGuest.ItemsSource = teams;
            comboGuest.DisplayMemberPath = "TeamName";

            listViewTeams.ItemsSource = standings;
            _standingsManager.UpdateStandings(_matchManager.GetMeches());
            _standingsManager.UpdateTable(listViewTeams, standings);
        }

        // Adding player to team
        private void BtnAddPlayer(object sender, RoutedEventArgs e)
        {
            if (listViewTeams.SelectedItem != null)
            {
                Standing selectedStanding = listViewTeams.SelectedItem as Standing;
                Team selectedTeam = selectedStanding.Team;

                string firstName = textBoxFirstName.Text;
                string lastName = textBoxLastName.Text;
                string position = comboPosition.Text;
                int teamID = selectedTeam.Id;

                Player player = new Player(firstName, lastName, position, teamID);
                SQLitePlayers.AddPlayer(connectionString, player);
                selectedTeam.Players.Add(player);

                MessageBox.Show($"{player.ShowPlayer()} has been added to {selectedTeam.TeamName}");
                listViewPlayers.Items.Refresh();
                ClearInputs();
            }
            else
            {
                MessageBox.Show("Select Team");
            }

        }

        // Remove player from team
        private void BtnDeletePlayer(object sender, RoutedEventArgs e)
        {
            if (listViewPlayers.SelectedItem != null)
            {
                Player player = listViewPlayers.SelectedItem as Player;
                SQLitePlayers.RemovePlayer(connectionString, player);
                Standing standing = listViewTeams.SelectedValue as Standing;
                int indexPlayer = listViewPlayers.SelectedIndex;
                Team team = standing.Team;
                team.Players.RemoveAt(indexPlayer);

                MessageBox.Show($"{player.ShowPlayer()} has been removed from {team.TeamName}");
                listViewPlayers.Items.Refresh();
            }
            else
            {
                MessageBox.Show("Select Player");
            }
        }

        // Add match and update standings
        private void BtnAddMatch(object sender, RoutedEventArgs e)
        {
            if (textBoxScoreGuest.Text.Equals("") || textBoxScoreHost.Text.Equals(""))
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
            else if (comboHost.SelectedItem == comboGuest.SelectedItem)
            {
                MessageBox.Show("Teams are the same! Change it!");
                return;
            }
            else
            {
                // Adding match
                Team hostTeam = comboHost.SelectedItem as Team;
                Team guestTeam = comboGuest.SelectedItem as Team;
                int scoreHost = Int32.Parse(textBoxScoreHost.Text);
                int scoreGuest = Int32.Parse(textBoxScoreGuest.Text);

                Match match = new Match(hostTeam.Id, guestTeam.Id, scoreHost, scoreGuest);
                SQLiteMatch.AddMatch(connectionString, match);
                _standingsManager.UpdateStats(match);

                MessageBox.Show("Match has been added");
                listViewTeams.Items.Refresh();
                _standingsManager.UpdateTable(listViewTeams, standings);
                ClearInputs();
            }
        }

        private void TeamsSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (Standing standings in listViewTeams.Items)
            {
                Standing selectedStanding = listViewTeams.SelectedItem as Standing;
                Team selectedTeam = selectedStanding.Team;
                listViewPlayers.ItemsSource = selectedTeam.Players;
            }
        }

        // allows u to input only one number
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

        // Sets window to fullscreen
        private void SetFullScreen()
        {
            WindowState = WindowState.Maximized;
        }

        // Clears inputs
        public void ClearInputs()
        {

            textBoxFirstName.Clear();
            textBoxLastName.Clear();
            comboPosition.Items.Clear();

            comboHost.SelectedItem = null;
            comboGuest.SelectedItem = null;
            textBoxScoreHost.Clear();
            textBoxScoreGuest.Clear();
        }
    }
}