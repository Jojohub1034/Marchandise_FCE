namespace Marchandise_FCE.Models
{
    public class Mmarchandise
    {
        public string refmarch { get; set; }
        public string nature { get; set; }
        public string codcat { get; set; }

        public Mmarchandise()
        {

        }

        public Mmarchandise(string refmarch, string nature, string codcat)
        {
            this.refmarch = refmarch;
            this.nature = nature;
            this.codcat = codcat;

        }
        public virtual ICollection<Menvoi> Envois { get; set; }
        public virtual Mcategorie Categorie { get; set; }
    }
}
