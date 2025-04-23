using System;
using System.Windows;
using System.Windows.Controls;

namespace Turnierplaner
{
    public partial class NeuesTurnierView : UserControl
    {
        public NeuesTurnierView()
        {
            InitializeComponent();
            InitDropdowns();
        }

        private void InitDropdowns()
        {
            // Gruppen & Felder von 1 bis 10
            for (int i = 1; i <= 10; i++)
            {
                GruppenBox.Items.Add(i);
                FelderBox.Items.Add(i);
            }

            // Startzeit in 15-Minuten-Schritten (07:00–21:45)
            for (int h = 7; h <= 21; h++)
            {
                for (int m = 0; m < 60; m += 15)
                {
                    StartzeitBox.Items.Add($"{h:00}:{m:00}");
                }
            }
        }

        private void Zurueck_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow?.ZeigeStartAnsicht();
        }

        private void Weiter_Click(object sender, RoutedEventArgs e)
        {
            // Validierung
            if (TurnierartBox.SelectedItem == null)
            {
                MessageBox.Show("Bitte Turnierart auswählen.");
                return;
            }

            if (GruppenBox.SelectedItem == null)
            {
                MessageBox.Show("Bitte Anzahl der Gruppen wählen.");
                return;
            }

            if (FelderBox.SelectedItem == null)
            {
                MessageBox.Show("Bitte Anzahl der Spielfelder wählen.");
                return;
            }

            if (StartzeitBox.SelectedItem == null)
            {
                MessageBox.Show("Bitte Startzeit wählen.");
                return;
            }

            if (!int.TryParse(PunkteBox.Text, out int punkte) || punkte <= 0)
            {
                MessageBox.Show("Bitte gültige Punktzahl zum Sieg eingeben.");
                return;
            }

            // In globale Variablen speichern
            App.Turnierart = ((ComboBoxItem)TurnierartBox.SelectedItem).Content.ToString();
            App.Gruppenanzahl = (int)GruppenBox.SelectedItem;
            App.Spielfelder = (int)FelderBox.SelectedItem;
            App.Startzeit = StartzeitBox.SelectedItem.ToString();
            App.PunkteZumSieg = punkte;
            App.Punktedifferenz = DifferenzCheck.IsChecked == true;

            var mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow?.MainContentDispatcher(new TeamEingabeView());

        }
    }
}
