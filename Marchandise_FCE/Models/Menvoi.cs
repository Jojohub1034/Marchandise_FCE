using System.ComponentModel.DataAnnotations;

namespace Marchandise_FCE.Models
{
    public class Menvoi
    {
        public int numenvoi { get; set; }
        public string idexp { get; set; }
        public string codegare { get; set; }
        public string refmarch { get; set; }
        public int nbenvoi { get; set; }
        public DateTime date { get; set; }
        //[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        [Required]
        [Range(0, 1000)]
        
        public decimal poids { get; set; }

        public string type_embal { get; set; }
        [DataType(DataType.Currency)]
        
        public decimal montant;
       

        

        //public List<int> prix = new List<int> { 150,200,250,300};


        public Menvoi()
        {

        }

        public Menvoi(int numenvoi)
        {
            this.numenvoi = numenvoi;

        }

        public Menvoi(int numenvoi, string idexp, string codegare, string refmarch, int nbenvoi, DateTime date, decimal poids, string type_embal)
        {
            this.numenvoi = numenvoi;
            this.idexp = idexp;
            this.codegare = codegare;
            this.refmarch = refmarch;
            this.nbenvoi = nbenvoi;
            this.date = date;
            this.poids = poids;
            this.type_embal = type_embal;

        }

        

        public Mexpediteur Expediteur { get; set; }
        public Mgare Gare { get; set; }
        public Mmarchandise Marchandise { get; set; }
    }
}
