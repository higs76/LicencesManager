using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicencesManager
{
    public class Licence
    {

        public string Nom { get; set; }
        public string Cle { get; set; }
        public DateTime? DateExpiration { get; set; }
        public string Observation { get; set; }


        public Licence()
        {
            Nom = string.Empty;
            Cle = string.Empty;
            Observation = string.Empty; // Initialisation par défaut
            DateExpiration = null; // Valeur par défaut pour la date
        }

    }
}
