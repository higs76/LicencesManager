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
using System.Windows.Shapes;
using Ookii.Dialogs.Wpf; // Ajoutez cet espace de noms


namespace LicencesManager
{
    /// <summary>
    /// Logique d'interaction pour ParametresWindow.xaml
    /// </summary>
    public partial class ParametresWindow : Window
    {
        public string CheminActuel { get; set; }

        public ParametresWindow(string cheminActuel)
        {
            InitializeComponent();
            CheminActuel = cheminActuel;
            CheminTextBox.Text = CheminActuel;
        }

        private void ChangerDossier_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new VistaFolderBrowserDialog
            {
                Description = "Sélectionnez le dossier de sauvegarde"
            };

            if ((bool)dialog.ShowDialog())
            {
                CheminTextBox.Text = dialog.SelectedPath;
            }
        }

        private void Enregistrer_Click(object sender, RoutedEventArgs e)
        {
            CheminActuel = CheminTextBox.Text;
            DialogResult = true;
            Close();
        }
    }
}
