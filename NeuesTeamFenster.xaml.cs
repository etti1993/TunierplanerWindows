using System.Windows;

namespace Turnierplaner
{
    public partial class NeuesTeamFenster : Window
    {
        public string Teamname { get; private set; } = "";
        public string Ansprechpartner { get; private set; } = "";

        public NeuesTeamFenster()
        {
            InitializeComponent();
        }

        private void Hinzufuegen_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TeamnameBox.Text) || string.IsNullOrWhiteSpace(KapitänBox.Text))
            {
                MessageBox.Show("Bitte alle Felder ausfüllen.");
                return;
            }

            Teamname = TeamnameBox.Text;
            Ansprechpartner = KapitänBox.Text;
            DialogResult = true;
            Close();
        }
    }
}
