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
        public string Nom { get; set; }
        public ObservableCollection<Dossier> SousDossiers { get; set; }
        public ObservableCollection<Licence> Licences { get; set; }

        public Dossier()
        {
            Nom=string.Empty;
            SousDossiers = new ObservableCollection<Dossier>();
            Licences = new ObservableCollection<Licence>();
        }
    }
}
