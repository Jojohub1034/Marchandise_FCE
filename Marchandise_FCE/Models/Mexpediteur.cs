namespace Marchandise_FCE.Models
{
    public class Mexpediteur
    {
        public string idexp { get; set; }
        public string nomexp { get; set; }
        public string cin { get; set; }
        public string tel { get; set; }



        public Mexpediteur()
        {
        }

        public Mexpediteur(string idexp)
        {
            this.idexp = idexp;
        }

        public Mexpediteur(string idexp, string nomexp, string cin, string tel)
        {
            this.idexp = idexp;
            this.nomexp = nomexp;
            this.cin = cin;
            this.tel = tel;

        }
           
        public virtual ICollection<Menvoi> Envois { get; set; }
    }
}
