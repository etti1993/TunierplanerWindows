using System.Windows;
using System.Windows.Controls;

namespace Turnierplaner
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ZeigeStartAnsicht();
        }

        public void ZeigeStartAnsicht()
        {
            // Startansicht mit Buttons
            var stack = new StackPanel
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center
            };

            var title = new TextBlock
            {
                Text = "Turnierplaner",
                FontSize = 28,
                FontWeight = FontWeights.Bold,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(0, 0, 0, 20)
            };

            var buttonNeu = new Button
            {
                Content = "Neues Turnier erstellen",
                Width = 200,
                Height = 40,
                Margin = new Thickness(0, 0, 0, 10)
            };
            buttonNeu.Click += NeuesTurnier_Click;

            var buttonLaden = new Button
            {
                Content = "Turnier laden",
                Width = 200,
                Height = 40
            };
            buttonLaden.Click += TurnierLaden_Click;

            stack.Children.Add(title);
            stack.Children.Add(buttonNeu);
            stack.Children.Add(buttonLaden);

            MainContent.Content = stack;
        }

        private void NeuesTurnier_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new NeuesTurnierView();
        }

        private void TurnierLaden_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Turnier laden ist noch in Arbeit!");
        }
        public void MainContentDispatcher(UserControl control)
        {
            MainContent.Content = control;
        }

    }
}
