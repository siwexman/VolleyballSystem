using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using VolleyballSystem.Interfaces;

namespace VolleyballSystem.Classes
{
    public class StandingsManager
    {
        public List<Standing> ListStandings { get; set; } = new List<Standing>();

        public void FIllWithTeams(List<Team> teams)
        {
            for (int i = 0; i < teams.Count(); i++)
            {
                ListStandings.Add(new Standing(teams[i]));
            }
        }

        public void UpdateTable(ListView listView, List<Standing> standings)
        {
            List<Standing> orderedStandings = new List<Standing>();
            orderedStandings = standings.OrderByDescending(s=>s.Points).ThenByDescending(s=>s.SetsWon).ToList();

            listView.Items.Refresh();

            listView.ItemsSource = orderedStandings;
        }

        public void UpdateStats(Match match)
        {
            Standing standingsHost = ListStandings.Find(s => s.Team.Id == match.HostTeamID);
            Standing standingsGuest = ListStandings.Find(s => s.Team.Id == match.GuestTeamID);

            // Incrementing MatchesPlayed
            standingsHost.MatchesPlayed++;
            standingsGuest.MatchesPlayed++;

            // Adding Score to Host Team
            standingsHost.SetsWon += match.ScoreHost;
            standingsHost.SetsLose += match.ScoreGuest;

            // Adding Score to Guest
            standingsGuest.SetsWon += match.ScoreGuest;
            standingsGuest.SetsLose += match.ScoreHost;

            if (match.ScoreHost > match.ScoreGuest) // who scored more
            {
                // true - host win
                if (match.ScoreHost - match.ScoreGuest > 1)
                {
                    standingsHost.Points += 3;
                }
                else
                {
                    // tie-break
                    standingsHost.Points += 2;
                    standingsGuest.Points += 1;
                }
            }
            else
            {
                // false - guest win
                if (match.ScoreGuest - match.ScoreHost > 1)
                {
                    standingsGuest.Points += 3;
                }
                else
                {
                    // tie-break
                    standingsGuest.Points += 2;
                    standingsHost.Points += 1;
                }
            }
        }

        public void UpdateStandings(List<Match> matches)
        {
            foreach (Match match in matches)
            {
                UpdateStats(match);
            }
        }
    }
}
