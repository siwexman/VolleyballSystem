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
using VolleyballSystem.Interfaces;

namespace VolleyballSystem.Pages
{
    /// <summary>
    /// Interaction logic for TeamPage.xaml
    /// </summary>
    public partial class TeamPage : Page
    {
        public TeamPage(Team selectedTeam)
        {
            InitializeComponent();
            DataContext = selectedTeam;

            List<Player> players = selectedTeam.Players;

            listViewPlayers.Items.Clear();
            listViewPlayers.ItemsSource = players;
        }
    }
}
