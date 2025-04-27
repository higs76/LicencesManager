using LicencesManager.Models;
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
using LicencesManager.Models;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace LicencesManager
{
    /// <summary>
    /// Logique d'interaction pour AjouterModifierDossier.xaml
    /// </summary>
    /// 
    public partial class AjouterModifierDossier : Window
    {
        public Dossier Dossier { get; set; }
        private readonly Dossier parentDossier;

        public AjouterModifierDossier(Dossier dossier = null, Dossier parent = null)
        {
            InitializeComponent();
            Dossier = dossier ?? new Dossier();
            parentDossier = parent;
            DataContext = Dossier;

            if (dossier != null)
            {
                NomTextBox.Text = dossier.Nom;
            }
        }

        private void Enregistrer_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(NomTextBox.Text))
            {
                MessageBox.Show("Le nom ne peut pas être vide.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Dossier.Nom = NomTextBox.Text;

            // Vérifier si un dossier avec le même nom existe déjà
            if (parentDossier != null && parentDossier.SousDossiers.Any(d => d.Nom == Dossier.Nom))
            {
                MessageBox.Show("Un dossier avec ce nom existe déjà.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Dossier.Nom = NomTextBox.Text;
            DialogResult = true;
            Close();
        }
    }
}
