using System;
using System.Collections.Generic;
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

namespace VolleyballSystem.Pages
{
    /// <summary>
    /// Interaction logic for AddPlayer.xaml
    /// </summary>
    public partial class AddPlayer : Page
    {
        Team team;
        public AddPlayer(Team selectedTeam)
        {
            InitializeComponent();
            DataContext = selectedTeam;
            team = selectedTeam;
        }

        private void CreatePlayer(object sender, RoutedEventArgs e)
        {
            // Logic for entering text
            if (FirstNameBox.Text == string.Empty)
            {
                MessageBox.Show("Enter First Name:");
                return;
            }
            else if (LastNameBox.Text == string.Empty)
            {
                MessageBox.Show("Enter Last Name:");
                return;
            }
            else if (PositionBox.Text == string.Empty)
            {
                MessageBox.Show("Enter position");
                return;
            }

            
            Player player = new Player(
            FirstNameBox.Text.Substring(0, 1).ToUpper() + FirstNameBox.Text.Substring(1).ToLower(),
            LastNameBox.Text.Substring(0, 1).ToUpper() + LastNameBox.Text.Substring(1).ToLower(),
            PositionBox.Text
            );

            team.Players.Add(player);

            MessageBox.Show($"{player.FirstName} {player.LastName} has been added to {team.TeamName}");
        }
    }
}
