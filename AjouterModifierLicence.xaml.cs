using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace LicencesManager
{
    /// <summary>
    /// Logique d'interaction pour AjouterModifierLicence.xaml
    /// </summary>
    public partial class AjouterModifierLicence : Window
    {
        public Licence Licence { get; set; }

        public AjouterModifierLicence(Licence licence = null)
        {
            InitializeComponent();
            Licence = licence ?? new Licence();
            DataContext = Licence;


            // Remplir les champs avec les valeurs existantes si on modifie une licence
            if (licence != null)
            {
                NomTextBox.Text = licence.Nom;
                CleTextBox.Text = licence.Cle;
                DateExpirationPicker.SelectedDate = licence.DateExpiration;
                ObservationTextBox.Text = licence.Observation;
            }
          
        }

        private void Enregistrer_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NomTextBox.Text))
            {
                MessageBox.Show("Le nom ne peut pas être vide.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Licence.Nom = NomTextBox.Text;
            Licence.Cle = CleTextBox.Text;
            Licence.DateExpiration = DateExpirationPicker.SelectedDate;
            Licence.Observation = ObservationTextBox.Text;
            DialogResult = true;
            Close();
        }

    }
}
