namespace Marchandise_FCE.Models
{
    public class Mgare
    {
        public string codegare { get; set; }
        public string nomgare { get; set; }
        public string abrevnom { get; set; }
        public int kilometrage { get; set; }
        public string codezone { get; set; }


        public Mgare()
        {

        }

        public Mgare(string codegare)
        {
            this.codegare = codegare;
        }

        public Mgare(string codegare, string nomgare, string abrevnom, int kilometrage, string codezone)
        {
            this.codegare = codegare;
            this.nomgare = nomgare;
            this.abrevnom = abrevnom;
            this.kilometrage = kilometrage;
            this.codegare = codegare;
            this.codezone = codezone;
        }
        public virtual Mzone Zones { get; set; }
        public virtual ICollection<Menvoi> Envois { get; set; }
        
        public virtual ICollection<Mgare> Gares { get; set; }
    }
}
