using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;
using LicencesManager.Models;

namespace LicencesManager;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public ObservableCollection<Dossier> Dossiers { get; set; }
    public ObservableCollection<Licence> Licences { get; set; }
    private string filePath = "licences.xml";
    private const int ClipboardClearDelay = 30; // Durée en secondes avant d'effacer le presse-papier



    public MainWindow()
    {
        InitializeComponent();
        Dossiers = new ObservableCollection<Dossier>();       
        DossiersTreeView.ItemsSource = Dossiers;

        // Ajouter un dossier initial
        Dossiers.Add(new Dossier { Nom = "Dossier Initial" });

        LoadLicences();
    }

    private void AjouterLicence_Click(object sender, RoutedEventArgs e)
    {
        var window = new AjouterModifierLicence();
        if (window.ShowDialog() == true)
        {
            var selectedDossier = DossiersTreeView.SelectedItem as Dossier;
            if (selectedDossier != null)
            {
                selectedDossier.Licences.Add(window.Licence);
            }
        }
    }

    private void AjouterDossier_Click(object sender, RoutedEventArgs e)
    {
        var window = new AjouterModifierDossier();
        if (window.ShowDialog() == true)
        {
            var selectedDossier = DossiersTreeView.SelectedItem as Dossier;
            if (selectedDossier != null)
            {
                selectedDossier.SousDossiers.Add(window.Dossier);
            }
            else
            {
                Dossiers.Add(window.Dossier);
            }
        }
    }

    private void ModifierLicence_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.Tag is Licence selectedLicence)
        {
            var window = new AjouterModifierLicence(selectedLicence);
            if (window.ShowDialog() == true)
            {
                LicencesDataGrid.Items.Refresh();
            }
        }
    }

    private void SupprimerLicence_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.Tag is Licence selectedLicence)
        {
            var result = MessageBox.Show("Êtes-vous sûr de vouloir supprimer cette licence ?", "Confirmation de suppression", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                var selectedDossier = DossiersTreeView.SelectedItem as Dossier;
                if (selectedDossier != null)
                {
                    selectedDossier.Licences.Remove(selectedLicence);
                }
            }
        }
    }

    private void DossiersTreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
    {
        if (e.NewValue is Dossier selectedDossier)
        {
            LicencesDataGrid.ItemsSource = selectedDossier.Licences;
        }
    }

    private void Sauvegarder_Click(object sender, RoutedEventArgs e)
    {
        SaveLicences();
        MessageBox.Show("Données sauvegardées avec succès.", "Sauvegarde", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    private async void CopierCle_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.Tag is string cle)
        {
            Clipboard.SetText(cle);
            MessageBox.Show("Clé copiée dans le presse-papier.", "Copier", MessageBoxButton.OK, MessageBoxImage.Information);
            await ClearClipboardAfterDelay();
        }
    }

    private async Task ClearClipboardAfterDelay()
    {
        StatusBarItem.Content = "Clé copiée. Disparition dans 30 secondes...";
        for (int i = ClipboardClearDelay; i >= 0; i--)
        {
            StatusBarItem.Content = $"Clé copiée. Disparition dans {i} secondes...";
            await Task.Delay(1000);
        }
        Clipboard.Clear();
        StatusBarItem.Content = "Ready.";
    }

    private void LoadLicences()
    {
        if (File.Exists(filePath))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Dossier>), new XmlRootAttribute("ArrayOfDossier"));
            using (StreamReader reader = new StreamReader(filePath))
            {
                Dossiers = (ObservableCollection<Dossier>)serializer.Deserialize(reader);
                DossiersTreeView.ItemsSource = Dossiers;
            }
        }
    }

    private void SaveLicences()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Dossier>), new XmlRootAttribute("ArrayOfDossier"));
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            serializer.Serialize(writer, Dossiers);
        }
    }

    protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
    {
        base.OnClosing(e);
        SaveLicences();
    }
}

