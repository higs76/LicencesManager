using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicencesManager.Models
{
    public class Dossier
    {
        public int Version {  get; set; }
        public string Nom { get; set; } = string.Empty;
        public ObservableCollection<Dossier> SousDossiers { get; set; }
        public ObservableCollection<Licence> Licences { get; set; }

        public Dossier()
        {            
            SousDossiers = new ObservableCollection<Dossier>();
            Licences = new ObservableCollection<Licence>();
        }
    }
}
