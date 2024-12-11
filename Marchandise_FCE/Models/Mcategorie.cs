namespace Marchandise_FCE.Models
{
    public class Mcategorie
    {
        public string codcat { get; set; }
        public string designcat { get; set; }

        public Mcategorie()
        {

        }

        public Mcategorie(string codcat)
        {
            this.codcat = codcat;
        }
        public Mcategorie(string codcat, string designcat)
        {
            this.codcat = codcat;
            this.designcat = designcat;
        }
        

        public virtual ICollection<Mmarchandise> Marchandises { get; set; }

        public virtual ICollection<Mtarif> Tarifs { get; set; }
    }
}
