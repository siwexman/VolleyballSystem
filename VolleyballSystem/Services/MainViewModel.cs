using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using VolleyballSystem.Classes;

namespace VolleyballSystem.Services
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Team> _teams;
        private Team _selectedTeam;

        public ObservableCollection<Team> Teams
        {
            get => _teams;
            set
            {
                _teams = value;
                OnPropertyChanged(nameof(Teams));
            }
        }

        public Team SelectedTeam
        {
            get => _selectedTeam;
            set
            {
                _selectedTeam = value;
                OnPropertyChanged(nameof(SelectedTeam));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public MainViewModel()
        {
            Teams = new ObservableCollection<Team>()
            {
                new Team{TeamName = "Lubcza"},
                new Team{TeamName = "Urzet" },
                new Team{TeamName = "AKS"},
                new Team{TeamName = "Rakszawa"},
                new Team{TeamName = "Bystrzaki"}
            };
        }
    }
}
