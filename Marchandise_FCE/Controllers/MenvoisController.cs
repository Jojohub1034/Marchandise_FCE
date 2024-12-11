using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Marchandise_FCE.Models;

using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;



namespace Marchandise_FCE.Controllers
{
    public class MenvoisController : Controller
    {
        private readonly AnnuaireContext _context;
        
        public MenvoisController(AnnuaireContext context)
        {
            _context = context;
        }

        //calcule montant


        // GET: Menvois
        public async Task<IActionResult> Index()
        {
            // Récupérer les envois avec les données liées
            var envois = await _context.Envois
                .Include(e => e.Expediteur)
                .Include(e => e.Gare)
                .Include(e => e.Marchandise)
                .ToListAsync();

            
            // Calculer le montant pour chaque envoi en parallèle
            foreach (var envoi in envois)
            
            {
                try
                {
                    envoi.montant = CalculMontant(envoi);
                }
                catch (Exception ex)
                {
                    
                    Console.WriteLine($"Erreur lors du calcul du montant pour l'envoi {envoi.numenvoi}: {ex.Message}");
                }
            };

           /* if ()
            {
                envois = envois.Where(e => e.Expediteur.nomexp.Contains() ||
                                           e.Gare.abrevnom.Contains())
                               .ToList();
            }*/

            return View(envois);
        }

        private decimal CalculMontant(Menvoi envoi)
        {
            // Vérifier les données
            if (envoi == null || envoi.Marchandise == null || envoi.Gare == null)
            {
                throw new ArgumentNullException("Envoi, Marchandise ou Gare est null");
            }

            // Récupérer la zone associée à la gare (ajuster la condition selon la relation)
            var zone = _context.Tarifs
                .FirstOrDefault(g => g.codezone == envoi.Gare.codezone); // Assurez-vous que la relation est correcte

            var tarif = _context.Tarifs
                .FirstOrDefault(t => t.codcat == envoi.Marchandise.codcat);

            if (tarif == null)
            {
                throw new Exception($"Tarif non trouvé pour la catégorie {envoi.Marchandise.codcat}");
            }

            return tarif.prixkilo * envoi.poids;
        }

        public IActionResult Facture()
        {
            ViewData["idexp"] = new SelectList(_context.Expediteurs, "idexp", "idexp");
            ViewData["codegare"] = new SelectList(_context.Gares, "codegare", "codegare");
            return View();
        }


        public IActionResult GeneratePdf(string idexp,string codegare, DateTime datedepart)
        {
            // Récupérer les envois en fonction de l'idexp et des dates
            var envois = _context.Envois
                .Include(e => e.Expediteur)
                .Include(e => e.Gare)
                .Include(e => e.Marchandise)
                .Where(e => (e.idexp == idexp) && (e.codegare == codegare) && (e.date == datedepart))
                .ToList();

            if(_context.Envois.Any(e => e.date.Equals(datedepart)))
            {
                MemoryStream stream = new MemoryStream();


                PdfWriter writer = new(stream);
                PdfDocument pdf = new PdfDocument(writer);
                Document document = new Document(pdf);

               // string imagePath = "~/img/Logo.jpeg";
                //Image img = new Image(ImageDataFactory.Create("~/img/Logo.jpeg"));

                // Ajouter le logo (vous pouvez personnaliser la mise en page)
                //document.Add(new Paragraph().Add(img).SetMarginTop(20));
                // Ajouter un titre
                document.Add(new Paragraph("Facture").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));

                // Créer un tableau avec le nombre de colonnes nécessaires
                var table = new Table(4); // Ajuste le nombre de colonnes selon tes besoins
                table.SetWidth(UnitValue.CreatePercentValue(80)); // Pour que le tableau prenne toute la largeur

                // Ajouter les en-têtes du tableau
                table.AddHeaderCell("Réference");
                table.AddHeaderCell("Nature");
                table.AddHeaderCell("Poids");
                table.AddHeaderCell("Montant");
                decimal totalpoids = 0;
                decimal totalmontant = 0;
                int i = 0;
                // Ajouter les données des envois dans le tableau
                foreach (var envoi in envois)
                {
                    if (i == 0)
                    {
                        document.Add(new Paragraph("Nom : " + envoi.Expediteur.nomexp)).SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT);
                        document.Add(new Paragraph("Destination : " + envoi.Gare.nomgare)).SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT);
                        document.Add(new Paragraph("Date : " + envoi.date.ToShortDateString())).SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT);
                    }
                    try
                    {
                        envoi.montant = CalculMontant(envoi);
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine($"Erreur lors du calcul du montant pour l'envoi {envoi.numenvoi}: {ex.Message}");
                    }

                    table.AddCell(envoi.Marchandise.refmarch);

                    table.AddCell(envoi.Marchandise.nature);
                    table.AddCell(envoi.poids.ToString());
                    table.AddCell(envoi.montant.ToString());

                    totalmontant += envoi.montant;
                    i++;
                }



                table.AddCell("");
                table.AddCell("Total");
                
                table.AddCell("Ariary");

                table.AddCell(totalmontant.ToString());
                // Ajouter le tableau au document
                document.Add(table);
                document.Add(new Paragraph("Merci de votre confiance !!").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));
                document.Close();
                byte[] pdfBytes = stream.ToArray();
                return File(pdfBytes, "application/pdf", "Facture_Envois.pdf");
            }
           
            else
            {
                ViewBag.Erreur = "L'expediteur n'a pas de marchandise à cette date";
                return View();
            }
            
            
        }

        // GET: Menvois/Create
        public IActionResult Create()
        {
            ViewData["idexp"] = new SelectList(_context.Expediteurs, "idexp", "idexp");
            ViewData["codegare"] = new SelectList(_context.Gares, "codegare", "codegare");
            ViewData["refmarch"] = new SelectList(_context.Marchandises, "refmarch", "refmarch");
            return View();
        }

        // POST: Menvois/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("numenvoi,idexp,codegare,refmarch,nbenvoi,date,poids,type_embal")] Menvoi menvoi)
        {
            try
            {
               
                    _context.Add(menvoi);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                
                
                
            }
            catch(Exception ex)
            {
                ViewData["idexp"] = new SelectList(_context.Expediteurs, "idexp", "idexp", menvoi.idexp);
                ViewData["codegare"] = new SelectList(_context.Gares, "codegare", "codegare", menvoi.codegare);
                ViewData["refmarch"] = new SelectList(_context.Marchandises, "refmarch", "refmarch", menvoi.refmarch);
                return View(menvoi);
            }
            
        }


        // GET: Menvois/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menvoi = await _context.Envois.FindAsync(id);
            if (menvoi == null)
            {
                return NotFound();
            }
            ViewData["idexp"] = new SelectList(_context.Expediteurs, "idexp", "idexp", menvoi.idexp);
            ViewData["codegare"] = new SelectList(_context.Gares, "codegare", "codegare", menvoi.codegare);
            ViewData["refmarch"] = new SelectList(_context.Marchandises, "refmarch", "refmarch", menvoi.refmarch);
            return View(menvoi);
        }

        // POST: Menvois/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("numenvoi,idexp,codegare,refmarch,nbenvoi,date,poids,type_embal")] Menvoi menvoi)
        {
            if (id != menvoi.numenvoi)
            {
                return NotFound();
            }

            if (_context.Envois.Any(z => z.numenvoi.Equals(id)))
            {
                try
                {
                    _context.Update(menvoi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenvoiExists(menvoi.numenvoi))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["idexp"] = new SelectList(_context.Expediteurs, "idexp", "idexp", menvoi.idexp);
            ViewData["codegare"] = new SelectList(_context.Gares, "codegare", "codegare", menvoi.codegare);
            ViewData["refmarch"] = new SelectList(_context.Marchandises, "refmarch", "refmarch", menvoi.refmarch);
            return View(menvoi);
        }

        // GET: Menvois/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menvoi = await _context.Envois
                .Include(m => m.Expediteur)
                .Include(m => m.Gare)
                .Include(m => m.Marchandise)
                .FirstOrDefaultAsync(m => m.numenvoi == id);
            if (menvoi == null)
            {
                return NotFound();
            }

            return View(menvoi);
        }

        // POST: Menvois/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var menvoi = await _context.Envois.FindAsync(id);
            if (menvoi != null)
            {
                _context.Envois.Remove(menvoi);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool MenvoiExists(int id)
        {
            return _context.Envois.Any(e => e.numenvoi == id);
        }

        public IActionResult Rapport()
        {
           /* int moisPrecedent = DateTime.Now.Month == 1 ? 12 : DateTime.Now.Month - 1;

            // Efficiently join tables and project required data using .Select()
            var rapportData = _context.Envois
                .Where(e => e.date.Month == moisPrecedent)
                .Select(e => new Menvoi // Use the designated model class
                { 
                    poids = e.poids,
                    refmarch = _context.Marchandises
                        .Where(m => m.refmarch == e.refmarch)
                        .Select(m => m.nature)
                        .FirstOrDefault(),
                    codegare = _context.Gares
                        .Where(g => g.codegare == e.codegare)
                        .Select(g => g.nomgare)
                        .FirstOrDefault()
                })
                .ToList();

            // Handle no data scenario gracefully
            if (!rapportData.Any())
            {
                // Optionally, display a custom message or redirect to an appropriate view
                return View("NoData", new { message = "Aucun envoi trouvé pour le mois précédent." }); // Example using "NoData" view
            }*/

            return View();
        }
        [HttpGet]
        public ActionResult Filtrermois(string mois, string garenom)
        {
       
            // Définir le mois actuel ou le mois précédent par défaut
            DateTime monthToShow = DateTime.Now.AddMonths(-1); // Mois précédent par défaut
            if (!string.IsNullOrEmpty(mois))
            {
                monthToShow = DateTime.ParseExact(mois, "yyyy-MM", null);
            }

            // Récupérer les envois pour le mois spécifié
            var envois = _context.Envois
                .Where(e => e.date.Year == monthToShow.Year && e.date.Month == monthToShow.Month)
                .Include(e => e.Marchandise)
                .Include(e => e.Gare)
                .ToList();

            // Filtrer par gare si une gare est sélectionnée
            if (!string.IsNullOrEmpty(garenom))
            {
                envois = envois.Where(e => e.codegare == garenom).ToList();
            }

            // Préparer les données pour la vue : un regroupement des marchandises et leur poids total
            var rapport = envois
                .GroupBy(e => new { e.Marchandise.refmarch, e.Marchandise.nature, e.Gare.nomgare })
                .Select(g => new
                {
                    MarchandiseRef = g.Key.refmarch,
                    MarchandiseNature = g.Key.nature,
                    GareNom = g.Key.nomgare,
                    PoidsTotal = g.Sum(e => e.poids)
                })
                .ToList();

            // Créer une liste de gares pour le filtre
            var gares = _context.Gares.ToList();

            // Créer une liste de mois pour le filtre (par exemple les 12 derniers mois)
            var _mois = Enumerable.Range(0, 12)
                .Select(i => DateTime.Now.AddMonths(-i))
                .Select(d => d.ToString("yyyy-MM"))
                .ToList();

            // Passer les données à la vue
            ViewBag.Gares = new SelectList(gares, "codegare", "nomgare");
            ViewBag.Mois = new SelectList(_mois);

            return View(rapport);
        }
    }
}

