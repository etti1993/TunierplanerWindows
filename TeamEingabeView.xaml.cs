using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Turnierplaner
{
    public partial class TeamEingabeView : UserControl
    {
        public TeamEingabeView()
        {
            InitializeComponent();
            teams = new List<TeamInfo>(TeamEingabeViewGlobal.Teams); // Bestehende Teams laden
            TeamTabelle.ItemsSource = teams;
        }

        private List<TeamInfo> teams;

        public class TeamInfo
        {
            public int Nummer { get; set; }
            public string Teamname { get; set; } = string.Empty;
            public string Ansprechpartner { get; set; } = string.Empty;
        }

        private void NeuesTeam_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new NeuesTeamFenster();
            if (dialog.ShowDialog() == true)
            {
                var neuesTeam = new TeamInfo
                {
                    Nummer = teams.Count + 1,
                    Teamname = dialog.Teamname,
                    Ansprechpartner = dialog.Ansprechpartner
                };

                teams.Add(neuesTeam);
                TeamEingabeViewGlobal.Teams.Add(neuesTeam); // In globale Liste einfügen
                TeamTabelle.Items.Refresh();
            }
        }

        private void TeamsLaden_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("API-Ladevorgang kommt später – hier Teams von WordPress holen.");
        }

        private void Zurueck_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow?.MainContentDispatcher(new NeuesTurnierView());
        }

        private void Weiter_Click(object sender, RoutedEventArgs e)
        {
            if (teams.Count == 0)
            {
                MessageBox.Show("Bitte zuerst Teams hinzufügen.");
                return;
            }

            TeamEingabeViewGlobal.Teams.Clear(); // Vorher leeren
            TeamEingabeViewGlobal.Teams.AddRange(teams); // Globale Teams aktualisieren

            var mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow?.MainContentDispatcher(new GruppeneinteilungView());
        }
    }

    // Globale statische Liste zum Zugriff in Gruppeneinteilung etc.
    public static class TeamEingabeViewGlobal
    {
        public static List<TeamEingabeView.TeamInfo> Teams { get; } = new();
    }
}
