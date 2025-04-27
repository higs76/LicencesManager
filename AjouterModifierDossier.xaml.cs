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

namespace LicencesManager
{
    /// <summary>
    /// Logique d'interaction pour AjouterModifierDossier.xaml
    /// </summary>
    /// 
    public partial class AjouterModifierDossier : Window
    {
        public Dossier Dossier { get; set; }

        public AjouterModifierDossier(Dossier dossier = null)
        {
            InitializeComponent();
            Dossier = dossier ?? new Dossier();
            DataContext = Dossier;

            if (dossier != null)
            {
                NomTextBox.Text = dossier.Nom;
            }
        }

        private void Enregistrer_Click(object sender, RoutedEventArgs e)
        {
            Dossier.Nom = NomTextBox.Text;
            DialogResult = true;
            Close();
        }
    }
}
