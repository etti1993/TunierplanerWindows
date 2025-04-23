using System.Configuration;
using System.Data;
using System.Windows;

namespace Turnierplaner
{
    public partial class App : Application
    {
        // Globale Variablen für das Turnier
        public static string Turnierart = "";
        public static int Gruppenanzahl = 0;
        public static int Spielfelder = 0;
        public static string Startzeit = "";
        public static int PunkteZumSieg = 0;
        public static bool Punktedifferenz = false;
    }
}
