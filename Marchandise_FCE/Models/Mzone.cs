namespace Marchandise_FCE.Models
{
    public class Mzone
    {
        public string codezone { get; set; }
        public string nomzone { get; set; }
        public string descriptzone { get; set; }

        public Mzone()
        {

        }

        public Mzone(string codezone)
        {
            this.codezone = codezone;
        }

        public Mzone(string codezone, string nomzone, string descriptzone)
        {
            this.codezone = codezone;
            this.nomzone = nomzone;
            this.descriptzone = descriptzone;
        }

        public virtual ICollection<Mgare> Gares { get; set; }

        public virtual ICollection<Mtarif> Tarifs { get; set; }
    }
}
