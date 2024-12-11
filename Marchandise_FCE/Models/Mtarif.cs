namespace Marchandise_FCE.Models
{
    public class Mtarif
    {
        public string idtarif { get; set; }
        public int prixkilo { get; set; }

        public  string codcat {  get; set; }
        public string codezone { get; set; }

        public Mtarif()
        {

        }

        public Mtarif(string idtarif, int prixkilo, string codcat, string codezone, Mzone zone, Mcategorie categorie)
        {
            this.idtarif = idtarif;
            this.prixkilo = prixkilo;
            this.codcat = codcat;
            this.codezone = codezone;
            Zone = zone;
            Categorie = categorie;
        }

        public Mzone Zone { get; set; }
        public Mcategorie Categorie { get; set; }

    }
}
