using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;
using LicencesManager.Models;

namespace LicencesManager;

public class GestionLicences
{
    private string filePath;
    private string topDossierName = "Mes Licences";
    private int currentVersion = 11; // Version actuelle des sauvegardes


    public GestionLicences(string filePath)
	{
        this.filePath = Path.Combine(filePath, "licences.xml");
    }

    public ObservableCollection<Dossier> LoadLicences()
    {
        if (File.Exists(filePath))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Dossier>), new XmlRootAttribute("ArrayOfDossier"));
            using (StreamReader reader = new StreamReader(filePath))
            {
                var dossiers = (ObservableCollection<Dossier>)serializer.Deserialize(reader);

                // Appliquer les migrations si nécessaire
                foreach (var dossier in dossiers)
                {
                    MigrateDossier(dossier);
                }

                return dossiers;
            }
        }
        return new ObservableCollection<Dossier>
            {
                new Dossier { Nom = topDossierName} // Ajouter un dossier initial par défaut
            };
        
    }

    public void SaveLicences(ObservableCollection<Dossier> dossiers)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Dossier>), new XmlRootAttribute("ArrayOfDossier"));
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            serializer.Serialize(writer, dossiers);
        }
    }

    private void MigrateDossier(Dossier dossier)
    {
        while (dossier.Version < currentVersion)
        {
            if (dossier.Version < 5)
            {
                MigrateToVersion5(dossier);
            }
            else if (dossier.Version < 10)
            {
                MigrateToVersion10(dossier);
            }
            else
            {
                //Si la version de migration n'a pas encore été incorporé.
                break;
            }
            
        }
    }

    private void MigrateToVersion5(Dossier dossier)
    {
        // Appliquer les migrations de la version 1 à la version 5
        dossier.Version = 5;
    }

    private void MigrateToVersion10(Dossier dossier)
    {
        // Appliquer les migrations de la version 5 à la version 10
        dossier.Version = 10;
    }
}
