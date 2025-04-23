using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Turnierplaner
{
    public partial class GruppeneinteilungView : UserControl
    {
        private List<List<TeamEingabeView.TeamInfo>> gruppen = new();

        public GruppeneinteilungView()
        {
            InitializeComponent();
        }

        private void GeneriereGruppen_Click(object sender, RoutedEventArgs e)
        {
            if (App.Gruppenanzahl <= 0)
            {
                MessageBox.Show("Ungültige Gruppenzahl.");
                return;
            }

            if (TeamEingabeViewGlobal.Teams.Count == 0)
            {
                MessageBox.Show("Keine Teams vorhanden.");
                return;
            }

            // Teams mischen
            var shuffled = TeamEingabeViewGlobal.Teams.OrderBy(_ => System.Guid.NewGuid()).ToList();

            gruppen = new();
            for (int i = 0; i < App.Gruppenanzahl; i++)
                gruppen.Add(new List<TeamEingabeView.TeamInfo>());

            for (int i = 0; i < shuffled.Count; i++)
                gruppen[i % App.Gruppenanzahl].Add(shuffled[i]);

            RenderGruppen();
        }

        private void RenderGruppen()
        {
            GruppenPanel.Children.Clear();

            for (int g = 0; g < gruppen.Count; g++)
            {
                var groupIndex = g;

                var listbox = new ListBox
                {
                    Width = 280,
                    Height = 250,
                    Margin = new Thickness(10),
                    BorderThickness = new Thickness(1),
                    BorderBrush = SystemColors.ActiveBorderBrush,
                    AllowDrop = true,
                    Tag = groupIndex
                };

                var label = new TextBlock
                {
                    Text = $"Gruppe {g + 1}",
                    FontSize = 16,
                    FontWeight = FontWeights.Bold,
                    Margin = new Thickness(0, 0, 0, 5)
                };

                var panel = new StackPanel();
                panel.Children.Add(label);
                panel.Children.Add(listbox);

                listbox.ItemsSource = gruppen[g];
                listbox.DisplayMemberPath = "Teamname";

                listbox.PreviewMouseMove += ListBox_PreviewMouseMove;
                listbox.Drop += ListBox_Drop;

                GruppenPanel.Children.Add(panel);
            }
        }

        private void ListBox_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && sender is ListBox lb && lb.SelectedItem is TeamEingabeView.TeamInfo team)
            {
                DragDrop.DoDragDrop(lb, new DataObject("team", team), DragDropEffects.Move);
            }
        }

        private void ListBox_Drop(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent("team") || sender is not ListBox zielListe)
                return;

            var team = (TeamEingabeView.TeamInfo)e.Data.GetData("team");

            // Entferne aus alter Gruppe
            foreach (var gruppe in gruppen)
            {
                if (gruppe.Remove(team)) break;
            }

            // Füge zur neuen Gruppe hinzu
            int zielIndex = (int)zielListe.Tag;
            gruppen[zielIndex].Add(team);

            RenderGruppen(); // Anzeige neu bauen
        }

        private void Zurueck_Click(object sender, RoutedEventArgs e)
        {
            var main = Application.Current.MainWindow as MainWindow;
            main?.MainContentDispatcher(new TeamEingabeView());
        }

        private void Weiter_Click(object sender, RoutedEventArgs e)
        {
            var main = Application.Current.MainWindow as MainWindow;
            main?.MainContentDispatcher(new TurnierÜbersichtView());
        }
    }
}
