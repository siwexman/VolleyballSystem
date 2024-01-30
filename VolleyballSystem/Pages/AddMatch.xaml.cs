using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using static VolleyballSystem.Interfaces.IPlayerRepository;

namespace VolleyballSystem.Pages
{
    /// <summary>
    /// Interaction logic for AddMatch.xaml
    /// </summary>
    public partial class AddMatch : Page
    {

        public AddMatch()
        {
            InitializeComponent();
            
            List<Team> teams = new List<Team>();
            StandingsManager st = new StandingsManager(new MockTeamRepository(), new MockPlayerRepository());
            teams = st.teams;

            comboHostTeams.Items.Clear();
            comboGuestTeams.Items.Clear();

            comboHostTeams.ItemsSource = teams;
            comboHostTeams.DisplayMemberPath = "TeamName";
            comboGuestTeams.ItemsSource = teams;
            comboGuestTeams.DisplayMemberPath = "TeamName";
        
        }

        private void btnAddMatch(object sender, RoutedEventArgs e)
        {
            if (comboHostTeams.SelectedItem != comboGuestTeams.SelectedItem)
            {
                MessageBox.Show(comboHostTeams.SelectedItem.ToString());
            }
            else if (Int32.Parse(textBoxScoreGuest.Text) > 3 || Int32.Parse(textBoxScoreHost.Text) > 3)
            {
                MessageBox.Show("Sets can't be greater than 3");
            }
            else
            {
                MessageBox.Show("Teams are the same! Change it!");
                return;
            }
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
    }
}
