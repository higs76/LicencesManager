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

namespace LicencesManager;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public ObservableCollection<Licence> Licences { get; set; }
    private string filePath = "licences.xml";
    private const int ClipboardClearDelay = 30; // Durée en secondes avant d'effacer le presse-papier

    public MainWindow()
    {
        InitializeComponent();
        Licences = new ObservableCollection<Licence>();
        LicencesDataGrid.ItemsSource = Licences;
        LoadLicences();
    }

    private void AjouterLicence_Click(object sender, RoutedEventArgs e)
    {
        // Ouvrir une fenêtre pour ajouter une nouvelle licence
        var window = new AjouterModifierLicence();
        if (window.ShowDialog() == true)
        {
            Licences.Add(window.Licence);
        }
    }

    private void ModifierLicence_Click(object sender, RoutedEventArgs e)
    {
        // Ouvrir une fenêtre pour modifier la licence sélectionnée
        if (LicencesDataGrid.SelectedItem is Licence selectedLicence)
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
                Licences.Remove(selectedLicence);
            }
        }
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
            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Licence>));
            using (StreamReader reader = new StreamReader(filePath))
            {
                Licences = (ObservableCollection<Licence>)serializer.Deserialize(reader);
                LicencesDataGrid.ItemsSource = Licences;
            }
        }
    }

    private void SaveLicences()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Licence>));
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            serializer.Serialize(writer, Licences);
        }
    }

    protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
    {
        base.OnClosing(e);
        SaveLicences();
    }

    private void Sauvegarder_Click(object sender, RoutedEventArgs e)
    {
        SaveLicences();
        MessageBox.Show("Données sauvegardées avec succès.", "Sauvegarde", MessageBoxButton.OK, MessageBoxImage.Information);
    }


}